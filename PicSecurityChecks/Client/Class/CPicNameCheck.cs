using Microsoft.VisualBasic;
using PicSecurityChecks.Shared;
using System.Text;


namespace PicSecurityChecks.Client.Class
{
    public class CPicNameCheck
    {

        private Utilities utilities { get; set; } = new Utilities();
        private String[] subStrings { get; set; }
        private String[] subLastNames { get; set; }
        private String[] subOtherNames { get; set; }
        private String[] subOtherLastNames { get; set; }

        public UniqueStatements CheckName(PIC_SecurityCheckNames check, ref int queryCount, string reg, char[] delimiters, UniqueStatements uniqueStatements)
        {
            StringBuilder CPIC_File;

            string VS = string.Empty;
            if (check.VSCheck.ToUpper() == "Y")
                VS = "/VS:Y";
            check.MiddleName = (check.MiddleName ?? string.Empty);
            if (check.MiddleName.ToUpper() == "KAUR" || check.MiddleName.ToUpper() == "SINGH")
                check.MiddleName = string.Empty;
            check.LastName = utilities.replaceWithSpace(check.LastName);
            check.FirstName = utilities.replaceWithSpace(check.FirstName);
            check.MiddleName = utilities.replaceWithSpace(check.MiddleName);

            CPIC_File = GenerateQuery(check.LastName, check.FirstName, check.MiddleName, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
            uniqueStatements.Add(CPIC_File.ToString());

            if (check.LastName.IndexOfAny(delimiters) > -1)
            {
                subStrings = check.LastName.Split(delimiters);
                foreach (var subString in subStrings)
                {
                    CPIC_File = GenerateQuery(subString.Trim(), check.FirstName, check.MiddleName, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                    uniqueStatements.Add(CPIC_File.ToString());
                }
            }

            if (check.OtherSurname != null)
                if (check.OtherSurname.Length > 0)
                {
                    check.OtherSurname = utilities.replaceWithSpace(check.OtherSurname);
                    CPIC_File = GenerateQuery(check.OtherSurname, check.FirstName, check.MiddleName, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                    uniqueStatements.Add(CPIC_File.ToString());

                    if (check.OtherSurname.IndexOfAny(delimiters) > -1)
                    {
                        subOtherLastNames = check.OtherSurname.Split(delimiters);
                        foreach (var subOtherLastName in subOtherLastNames)
                        {
                            CPIC_File = GenerateQuery(subOtherLastName.Trim(), check.FirstName, check.MiddleName, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                            uniqueStatements.Add(CPIC_File.ToString());
                        }
                    }
                }
            if (check.OtherName != null)
                if (check.OtherName.Length > 0)
                {
                    check.OtherName = utilities.replaceWithSpace(check.OtherName);
                    CPIC_File = GenerateQuery(check.LastName, check.OtherName, default, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                    uniqueStatements.Add(CPIC_File.ToString());

                    if (check.OtherName.IndexOfAny(delimiters) > -1)
                    {
                        subOtherNames = check.OtherName.Split(delimiters);
                        foreach (var subOtherName in subOtherNames)
                        {
                            CPIC_File = GenerateQuery(check.LastName, subOtherName, default, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                            uniqueStatements.Add(CPIC_File.ToString());
                            if (check.LastName.IndexOfAny(delimiters) > -1)
                            {
                                subLastNames = check.LastName.Split(delimiters);
                                foreach (var subLastName in subLastNames)
                                {
                                    CPIC_File = GenerateQuery(subLastName, subOtherName, default, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                                    uniqueStatements.Add(CPIC_File.ToString());
                                }
                            }
                        }
                    }
                }
            if (check.OtherName != null && check.OtherSurname != null)
            {
                if (check.OtherName.Length > 0 && check.OtherSurname.Length > 0)
                {
                    CPIC_File = GenerateQuery(check.OtherSurname, check.OtherName, default, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                    uniqueStatements.Add(CPIC_File.ToString());
                    subOtherLastNames = check.OtherSurname.Split(delimiters);
                    subOtherNames = check.OtherName.Split(delimiters);
                    foreach (var subOtherLastName in subOtherLastNames)
                        foreach (var subOtherName in subOtherNames)
                        {
                            CPIC_File = GenerateQuery(subOtherLastName, subOtherName, default, check.dob.ToString("yyyyMMdd"), VS, reg, ref queryCount, check.Gender);
                            uniqueStatements.Add(CPIC_File.ToString());
                        }
                }
            }

            return uniqueStatements;

        }

        private static StringBuilder GenerateQuery(string SubNames, string GivenName1, string GivenName2, string DOB, string VS, string RegNo, ref int QueryCount, string Gender)

        {

            StringBuilder NameGenerated;

            NameGenerated = new StringBuilder("");
            QueryCount += 1;
            NameGenerated = NameGenerated.Append("Q PERS+ SNME:");

            NameGenerated = NameGenerated.Append(SubNames);
            NameGenerated = NameGenerated.Append("/G1:");
            NameGenerated = NameGenerated.Append(GivenName1);
            NameGenerated = NameGenerated.Append("/G2:");
            if (GivenName2 is not null)
            {
                NameGenerated = NameGenerated.Append(GivenName2);
            }
            NameGenerated = NameGenerated.Append("/DOB:");
            NameGenerated = NameGenerated.Append(DOB);

            if (Gender.ToUpper() == "MALE")
                Gender = "M";
            if (Gender.ToUpper() == "FEMALE")
                Gender = "F";

            if (Strings.Right(VS, 1) == "Y")
            {
                NameGenerated = NameGenerated.Append(string.Format("/SEX:{0}", Gender));
            }
            else
            {
                NameGenerated = NameGenerated.Append("/SEX:U");
            }
            NameGenerated = NameGenerated.Append("/REM:");
            NameGenerated = NameGenerated.Append(RegNo);
            NameGenerated = NameGenerated.Append("/QREASON:INV");
            NameGenerated = NameGenerated.Append(Constants.vbCrLf);

            NameGenerated = NameGenerated.Append("Q CNI SNME:");
            NameGenerated = NameGenerated.Append(SubNames);
            NameGenerated = NameGenerated.Append("/G1:");
            NameGenerated = NameGenerated.Append(GivenName1);
            NameGenerated = NameGenerated.Append("/G2:");
            if (GivenName2 is not null)
            {
                NameGenerated = NameGenerated.Append(GivenName2);
            }
            NameGenerated = NameGenerated.Append("/DOB:");
            NameGenerated = NameGenerated.Append(DOB);
            if (Gender.ToUpper() == "MALE")
                Gender = "M";
            if (Gender.ToUpper() == "FEMALE")
                Gender = "F";

            if (Strings.Right(VS, 1) == "Y")
            {
                NameGenerated = NameGenerated.Append(string.Format("/SEX:{0}", Gender));
            }
            else
            {
                NameGenerated = NameGenerated.Append("/SEX:U");
            }
            // NameGenerated = NameGenerated.Append("/SEX:U")
            NameGenerated = NameGenerated.Append(VS);
            NameGenerated = NameGenerated.Append("/REM:");
            NameGenerated = NameGenerated.Append(RegNo);
            NameGenerated = NameGenerated.Append(" I");
            NameGenerated = NameGenerated.Append(Constants.vbCrLf);
            return NameGenerated;
        }

    }
}

using System;
using Microsoft.AspNetCore.Components;
using PicSecurityChecks.Shared;
using System.Reflection.Metadata;

namespace PicSecurityChecks.Client.Class
{
    public class Utilities
    {
        public char[] _delimiters { get; set; }

        public string StripAlpha(string AlphaCheck)
        {
            string value = string.Empty;
            foreach (char c in AlphaCheck.ToCharArray())
            {
                if (c < '0' || c > '9')
                {
                    value = value + c;
                }
                else
                    value = value + c;
            }
            return value;
        }

        public string replaceWithSpace(string name)
        {
            name = name.Replace("-", " ");
            name = name.Replace("'", "");
            return name;
        }

        public List<SearchParam> NameCombos(PIC_SecurityCheckNames picName, char[] delimiters)
        {
            SearchParam param = new SearchParam();
            _delimiters = delimiters;
            List<SearchParam> paramList = new List<SearchParam>();

            //These will not change for all names.
            param.Gender = picName.Gender;
            param.DOB = picName.dob;

            string[] OtherLastNames = picName.OtherSurname.Split(delimiters);
            string[] OtherNames = picName.OtherName.Split(delimiters);
            string[] firstNames = picName.FirstName.Split(delimiters);
            string[] lastNames = picName.LastName.Split(delimiters);

            foreach (string name in firstNames)
            {
                param.FirstName = FirstNameStripper(name.Trim());
                param.LastName = LastNameStripper(picName.LastName);
                param.Gender = picName.Gender;
                param.DOB = picName.dob;
                paramList.Add(param);
                param = new SearchParam();

                if (lastNames.Count() > 1)
                    foreach (string last in lastNames)
                    {
                        param.FirstName = FirstNameStripper(name.Trim());
                        param.LastName = LastNameStripper(last.Trim());
                        paramList.Add(param);
                        param = new SearchParam();
                    }

                if (OtherLastNames.Count() > 0)
                    foreach (string last in OtherLastNames)
                    {
                        if (last.Length > 0)
                        {
                            param.FirstName = FirstNameStripper(name.Trim());
                            param.LastName = LastNameStripper(last.Trim());
                            paramList.Add(param);
                            param = new SearchParam();
                        }
                    }

            }

            if (OtherNames.Count() > 0)
                foreach (string otherName in OtherNames)
                {
                    if (otherName.Length > 0)
                    {
                        param.FirstName = FirstNameStripper(otherName.Trim());
                        param.LastName = LastNameStripper(picName.LastName);
                        param.Gender = picName.Gender;
                        param.DOB = picName.dob;
                        paramList.Add(param);
                        param = new SearchParam();

                        if (lastNames.Count() > 1)
                            foreach (string last in lastNames)
                            {
                                param.FirstName = FirstNameStripper(otherName.Trim());
                                param.LastName = LastNameStripper(last.Trim());
                                paramList.Add(param);
                                param = new SearchParam();
                            }

                        if (OtherLastNames.Count() > 0)
                            foreach (string last in OtherLastNames)
                            {
                                if (last.Length > 0)
                                {
                                    param.FirstName = FirstNameStripper(otherName.Trim());
                                    param.LastName = LastNameStripper(last.Trim());
                                    paramList.Add(param);
                                    param = new SearchParam();
                                }
                            }
                    }
                }

            return paramList;
        }

        public string FirstNameStripper(string name)
        {
            if (name.Length > 3)
            {
                return name.Substring(0, 3) + "%";
            }
            else
            {
                return name + "%";
            }
        }

        public string LastNameStripper(string name)
        {
            
            foreach (char limit in _delimiters)
            {
                name = name.Replace(limit, '%');
            }
            return name;
        }
    }
}

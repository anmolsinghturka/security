using Microsoft.AspNetCore.Components;
using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Client.Services;
using PicSecurityChecks.Shared;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace PicSecurityChecks.Client.Class
{
    public class BuildNiche
    {
        public INicheCallsDataService callsDataService { get; set; }
        public Niche _niche { get; set; } = new Niche();
        public HttpClient client { get; set; }
        public Utilities utilities { get; set; } = new Utilities();
        public SearchParam searchParam { get; set; }
        public List<NicheCharge> nicheCharges { get; set; } = new List<NicheCharge>();
        public NicheCharge NicheCharge { get; set; } = new NicheCharge();

        public List<SearchParam> searchParams { get; set; }
        public List<Niche> niches { get; set; } = new List<Niche>();


        public async Task<List<Niche>> getDataFromNicheByName(PIC_SecurityCheckNames pic_SecurityCheckName, Niche niche, char[] delimiters, INicheCallsDataService NicheCallsDataService)
        {
            StringBuilder sql = new StringBuilder();
            callsDataService = NicheCallsDataService;
            _niche = niche;
            List<Niche> niches = new List<Niche>();
            searchParams = utilities.NameCombos(pic_SecurityCheckName, delimiters);

            foreach (SearchParam param in searchParams)
            {
                //await Fetch(param);
                //sql.Append("select GPerson__id as PersonId, GPersonName__id, gOccurrence.OccurrenceFileNo as CaseNo, gOccurrence.id as Id, ");
                //sql.Append("   GPersonName.Gender, GPersonName.DateOfBirthG as DateOfBirth, Gperson.NationalId as FPS, GPersonName.Type1G, ");
                //sql.Append("   GPersonName.Surname as Name, GPersonName.Given1 , GPersonName.Given2 , GOccIvGPerson.Classification, GOccIvGPerson.ClassificationG, ");
                //sql.Append("   GPersonName.SearchScore, Person.CautionSummaryLabel, Occurrence.OccurrenceStdOccTypeRId_L as OccurrenceType ");
                //sql.Append($" from GPersonName.ScoredName(Surname='{param.LastName.Replace("'", "''")} ', Given1='{param.FirstName.Replace("'", "''")}')");
                //sql.Append(" LEFT JOIN (GPerson LEFT JOIN Person LEFT JOIN (GOccIvGPerson left join (gOccurrence left join Occurrence)))");
                //sql.Append($" where GPersonName.Surname LIKE '{param.LastName.Replace("'", "''")}' ");
                //sql.Append($" and GPersonName.Given1 LIKE '{param.FirstName.Replace("'", "''")}' ");
                //sql.Append(" and gOccurrence.OccurrenceFileNo <> '' ");
                //sql.Append(" order by GPersonName.DateOfBirthG, gOccurrence.OccurrenceFileNo");

                try
                {
                    NicheReturnedData result = await callsDataService.GetNicheQuery(param.LastName, param.FirstName);

                    string temp = result.ReturnedData;
                    temp = CleanXML(temp);

                    DataSet ds = new DataSet();
                    StringReader reader = new StringReader(temp);

                    sql.Clear();

                    ds.ReadXml(reader);

                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            var test = dr["SearchScore"];
                            if (int.Parse(dr["SearchScore"].ToString()) > 0)
                            {
                                _niche = new Niche();
                                if (dt.Columns.IndexOf("Id") >= 0)
                                    _niche.Id = dr["Id"].ToString();
                                else
                                    _niche.Id = "0";
                                _niche.PersonId = dr["PersonId"].ToString();
                                if (dr["CaseNo"] is null)
                                    _niche.OccurrenceFileNo = "No CaseFile Number found";
                                else
                                    _niche.OccurrenceFileNo = dr["CaseNo"].ToString();
                                _niche.SurName = dr["Name"].ToString();
                                _niche.FirstName = dr["Given1"].ToString();
                                if (dt.Columns.IndexOf("Gender") >= 0)
                                    if (dr["Gender"] is null)
                                        _niche.Gender = "";
                                    else
                                        _niche.Gender = dr["Gender"].ToString();
                                else
                                    _niche.Gender = "";
                                if (dt.Columns.IndexOf("DateOfBirth") >= 0)
                                    if (dr["DateOfBirth"] is null)
                                        _niche.DateOfBirth = "";
                                    else
                                        _niche.DateOfBirth = dr["DateOfBirth"].ToString();
                                else
                                    _niche.DateOfBirth = "";
                                _niche.SurName = dr["Name"].ToString();
                                _niche.Involvement = dr["ClassificationG"].ToString();
                                _niche.SearchScore = dr["SearchScore"].ToString();
                                _niche.Flag = dr["CautionSummaryLabel"].ToString();
                                _niche.OccurrenceType = dr["OccurrenceType"].ToString();

                                if (dt.Columns.IndexOf("FPS") >= 0)
                                    if (!(dr["FPS"].ToString() is null))
                                        _niche.FPS = dr["FPS"].ToString();
                                    else
                                        _niche.FPS = string.Empty;

                                FetchCharges(_niche.Id, _niche.PersonId);
                                niches.Add(_niche);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    if (ex.InnerException == null)
                    {
                        _niche.errMessage = "Error getting data from web API " + ex.Message;

                    }
                    else
                    {
                        _niche.errMessage = ex.InnerException.Message;
                    }
                }


            }

            return niches;
        }

        public string CleanXML(string xml)
        {
            xml = xml.Replace("\\\r", "");
            xml = xml.Replace("\\\n", "");
            xml = xml.Replace("\\\n  ", "");
            xml = xml.Replace("\\\n    ", "");
            xml = xml.Replace("\\\t", "");
            xml = xml.Replace("\"", "");
            xml = xml.Replace("\\\\", "\\");
            xml = xml.Replace("\\\\\"", "\\\"");

            return xml;
        }

        public async Task Fetch(SearchParam param)
        {
            StringBuilder sql = new StringBuilder();

            //sql.Append("select GPerson__id as PersonId, GPersonName__id, gOccurrence.OccurrenceFileNo as CaseNo, gOccurrence.id as Id, ");
            //sql.Append("   GPersonName.Gender, GPersonName.DateOfBirthG as DateOfBirth, Gperson.NationalId as FPS, GPersonName.Type1G, ");
            //sql.Append("   GPersonName.Surname as Name, GPersonName.Given1, GPersonName.Given2, GOccIvGPerson.Classification, GOccIvGPerson.ClassificationG, ");
            //sql.Append("   GPersonName.SearchScore, Person.CautionSummaryLabel, Occurrence.OccurrenceStdOccTypeRId_L ras OccurrenceType ");
            //sql.Append($" from GPersonName.ScoredName(Surname='{param.LastName.Replace("'", "''")} ', Given1='{param.FirstName.Replace("'", "''")}')");
            //sql.Append(" LEFT JOIN (GPerson LEFT JOIN Person LEFT JOIN (GOccIvGPerson left join (gOccurrence left join Occurrence)))");
            //sql.Append($" where GPersonName.Surname LIKE '{param.LastName.Replace("'", "''")}' ");
            //sql.Append($" and GPersonName.Given1 LIKE '{param.FirstName.Replace("'", "''")}' ");
            //sql.Append(" and gOccurrence.OccurrenceFileNo <> '' ");
            //sql.Append(" order by GPersonName.DateOfBirthG, gOccurrence.OccurrenceFileNo");

            try
            {
                NicheReturnedData result = await callsDataService.GetNicheQuery(param.LastName, param.FirstName);
                _niche = new Niche();
                string temp = result.ReturnedData;
                temp = CleanXML(temp);

                DataSet ds = new DataSet();
                StringReader reader = new StringReader(temp);

                sql.Clear();

                ds.ReadXml(reader);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        var test = dr["SearchScore"];
                        if (int.Parse(dr["SearchScore"].ToString()) > 0)
                        {
                            _niche = new Niche();
                            if (dt.Columns.IndexOf("Id") >= 0)
                                _niche.Id = dr["Id"].ToString();
                            else
                                _niche.Id = "0";
                            _niche.PersonId = dr["PersonId"].ToString();
                            if (dr["CaseNo"] is null)
                                _niche.OccurrenceFileNo = "No CaseFile Number found";
                            else
                                _niche.OccurrenceFileNo = dr["CaseNo"].ToString();
                            if (dt.Columns.IndexOf("Gender") >= 0)
                                if (dr["Gender"] is null)
                                    _niche.Gender = "";
                                else
                                    _niche.Gender = dr["Gender"].ToString();
                            else
                                _niche.Gender = "";
                            if (dt.Columns.IndexOf("DateOfBirth") >= 0)
                                if (dr["DateOfBirth"] is null)
                                    _niche.DateOfBirth = "";
                                else
                                    _niche.DateOfBirth = dr["DateOfBirth"].ToString();
                            else
                                _niche.DateOfBirth = "";
                            _niche.Involvement = dr["ClassificationG"].ToString();
                            _niche.SearchScore = dr["SearchScore"].ToString();
                            _niche.Flag = dr["CautionSummaryLabel"].ToString();
                            _niche.OccurrenceType = dr["OccurrenceType"].ToString();

                            if (dt.Columns.IndexOf("FPS") >= 0)
                                if (!(dr["FPS"].ToString() is null))
                                    _niche.FPS = dr["FPS"].ToString();

                            FetchCharges(_niche.Id, _niche.PersonId);
                            niches.Add(_niche);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    _niche.errMessage = "Error getting data from web API " + ex.Message;

                }
                else
                {
                    _niche.errMessage = ex.InnerException.Message;
                }
            }
        }

        private async void FetchCharges(string Id, string PersonId)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("SELECT SectionMergedNS, ChargeWordingNSG, ChargeWordingS, GPChargeStdChargeRId_L ");
            sql.Append(" FROM gPersonCharge ");
            sql.Append($" WHERE WId = '{PersonId}'");
            sql.Append($" AND GPChargeOccRId = '{Id}'");
            NicheReturnedData result = await callsDataService.GetOtherNiche(sql.ToString());
            result.ReturnedData = CleanXML(result.ReturnedData);
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(result.ReturnedData);

                foreach (XmlNode node in xmlDoc.SelectNodes("//Main"))
                {
                    if (!(node.SelectSingleNode("ChargeWordingNSG").ToString() is null) || !(node.SelectSingleNode("SectionMergedNS").ToString() is null))
                        if (node.SelectSingleNode("ChargeWordingNSG").InnerXml != "" || node.SelectSingleNode("SectionMergedNS").InnerXml != "")
                            NicheCharge.Charge = $"{node.SelectSingleNode("SectionMergedNS").InnerXml} {node.SelectSingleNode("ChargeWordingNSG").InnerXml}";
                    if (!(node.SelectSingleNode("GPChargeStdChargeRId_L").ToString() is null))
                        if (node.SelectSingleNode("GPChargeStdChargeRId_L").InnerXml != "")
                            NicheCharge.Charge = $"{node.SelectSingleNode("GPChargeStdChargeRId_L").InnerXml}";
                    _niche.Charges.Add(NicheCharge);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    _niche.errMessage = "Error getting data from web API " + ex.Message;
                else
                    _niche.errMessage = ex.InnerException.Message;
            }

        }
    }
}
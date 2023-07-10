using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Class
{
    public class BuildINet
    {
        public IInetDataService inetData { get; set; }
        private List<Inet> inets { get; set; }
        private Inet inet { get; set; } = new Inet();
        private List<SearchParam> searchParams { get; set; } = new List<SearchParam>();
        private Utilities Utilities { get; set; } = new Utilities();

        public async Task<List<Inet>> GetInetResults(PIC_SecurityCheckNames picCheck, char[] delimiters, IInetDataService inetDataService)
        {
            inetData = inetDataService;
            inets = new List<Inet>();
            inet = new Inet();

            searchParams = Utilities.NameCombos(picCheck, delimiters);

            foreach (SearchParam param in searchParams)
            {
                await Fetch(param);
            }

            return inets;
        }

        private async Task Fetch(SearchParam param)
        {
            try
            {
                List<Inet> resultInet = (await inetData.GetInet(param.FirstName, param.LastName)).ToList();
                foreach (Inet inetr in resultInet)
                {
                    if (CheckRole(inetr.CMEM_ROLE) || inetr.CMEM_ROLE == "")
                    {
                        if (inet.Name is null)
                            inet.Name = "";
                        else
                            inet.Name = inetr.Name;
                        inet.CaseNo = inetr.CaseNo;
                        inet.OtherInformation = inetr.OtherInformation;
                        inet.system = inetr.system;
                        if (param.DOB.ToShortDateString() == "")
                            inet.DateOfBirth = inetr.DateOfBirth;
                        else
                            inet.DateOfBirth = param.DOB.ToShortDateString();
                        if (param.Gender == "" || param.Gender is null)
                            inet.Gender = inetr.Gender;
                        else
                            inet.Gender = param.Gender;

                        inets.Add(inet);
                    }
                }
            }
            catch (Exception ex)
            {

            }


        }

        private bool CheckRole(string role)
        {
            bool result = false;
            string RolesNeededToShowName = "ARRESTED ON WARRANT,PAROLEE,PROBATION,ARRESTED,UNLAWFULLY AT LARGE,MARIHUANA TRAFFICKER,COCAINE TRAFFICKER,HEROIN TRAFFICKER," +
                "OTHER TRAFFICKER,MARIHUANA GROWER,GRAFFITI ARTIST,ASSOCIATE,DRIVER,BICYCLIST,PASSENGER,PEDESTRIAN,VEHICLE OWNER,FAC APPLICANT,NOT PHYSICALLY CHECKED,CAU PAROLE INFO";
            string[] roles = RolesNeededToShowName.Split(',');

            foreach (string roleToShow in roles)
                if (roleToShow == role)
                    result = true;


            return result;
        }
    }
}

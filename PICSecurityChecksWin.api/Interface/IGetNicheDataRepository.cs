using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Interface
{
    public interface IGetNicheDataRepository
    {
        Task<NicheReturnedData> GetData(string lastName, string firstName);
        Task<NicheReturnedData> GetDataByPath(string path);
    }
}

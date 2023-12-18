using Dapper;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using System.Data;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IMSDatabase msDatabase) : base(msDatabase)
        {
        }
        public override async Task<BasePage<TEntityInPage>> GetPageAsync<TEntityInPage>(BasePagingArgument basePagingArgument)
        {
            // Tạo connection
            var connection = await _msDatabase.GetOpenConnectionAsync();

            string tableName = typeof(Comment).Name;

            string procedure = ProcedureResource.GetPaging(tableName);

            // Tạo tham số đầu vào 
            // IN _offset: Số bản ghi bị bỏ qua
            // IN _limit: Số bản ghi được lấy
            // IN employeeSearchTerm: Từ khóa tìm kiếm, theo employeeCode hoặc FullName
            // OUT TotalRecord: Tổng số bản ghi tìm thấy
            var parameters = new DynamicParameters();
            int offset = (basePagingArgument.PageNumber - 1) * basePagingArgument.PageSize;
            parameters.Add("_offset", offset);
            parameters.Add("_limit", basePagingArgument.PageSize);
            parameters.Add("searchTerm", basePagingArgument.SearchTerm ?? "");
            parameters.Add("newsId", basePagingArgument.NewsId == null ? null : basePagingArgument.NewsId);
            parameters.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {

                // Gọi procedure 
                IEnumerable<TEntityInPage> res = await connection.QueryAsync<TEntityInPage>(
                    procedure,
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Lấy tổng số trang 
                var totalRecord = parameters.Get<int>("totalRecord");

                // trả về kết quả
                return new BasePage<TEntityInPage>(totalRecord, res);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }
}

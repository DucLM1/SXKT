using System;
using System.Collections.Generic;
using System.Text;

namespace SXKT.Infrastructure.Databases.PostgreDB.Dto
{
    public interface IDtoDal
    {
        /// <summary>
        /// Gọi postgre function
        /// </summary>
        /// <typeparam name="T">Kiểu của phần tử trong danh sách trả về</typeparam>
        /// <param name="spName"></param>
        /// <param name="condition"></param>
        /// <returns>Danh sách phần tử</returns>
        List<T> QuerySP<T>(string spName, object condition) where T : class;
        List<T> QuerySP<T>(string spName, object condition, string connectionString) where T : class;
        /// <summary>
        /// Gọi postgre function và trả về số bản ghi bị thay đổi
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns>Số bản ghi bị thay đổi</returns>
        int ExecuteSP(string spName, object param);

        int ExecuteSP(string spName, object param, string connectionString);
        /// <summary>
        /// Gọi postgre function và trả về giá trị của cột đầu tiên của row đầu tiền trong kết quả của function đó
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="connectionString"></param>
        /// <returns>Giá trị của cột đầu tiên của row đầu tiền trong kết quả của postgre function</returns>
        object ExecuteScalarSP(string spName, object param);
        object ExecuteScalarSP(string spName, object param, string connectionString);
      
    }
}

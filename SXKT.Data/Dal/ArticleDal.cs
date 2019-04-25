using SXKT.Data.Dal.Interfaces;

namespace SXKT.Data.Dal
{
    public class ArticleDal : IArticleDal
    {
        public string DucLMtest(int articleId)
        {
            //    return PostgreDalHelper.QuerySP<int>("func_fe_articles_getlist_DucLM_test",new { _id = articleId}, PostgresSQL.DBPosition.Slave)
            //}
            return string.Empty;
        }
    }
}
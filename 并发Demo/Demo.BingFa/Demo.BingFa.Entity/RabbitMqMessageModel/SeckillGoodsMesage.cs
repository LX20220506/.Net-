namespace Demo.BingFa.Entity.RabbitMqMessageModel
{
    /// <summary>
    /// 用来再消息队列中传递消息(存在问题，内容设计的不合理，应该将订单信息加到里面)
    /// </summary>
    public class SeckillGoodsMesage
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid;
        /// <summary>
        /// 商品id
        /// </summary>
        public int goodsid;
    }
}

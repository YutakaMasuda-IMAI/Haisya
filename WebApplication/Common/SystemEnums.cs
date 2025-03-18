namespace WebApplication.Common
{
    public class SystemEnums
    {

        /// <summary>
        /// T_Portal_InfoのPortal_Kubun
        /// </summary>
        public enum PortalKubun
        {
            配車WEB = 1,
            請求WEB = 2,
            連携WEB = 3
        }

        /// <summary>
        /// T_Portal_InfoのDisplayFlag
        /// </summary>
        public static class DisplayFlag
        {
            public const int Visible = 0;
            public const int Hidden = 1;
        }

    }
}

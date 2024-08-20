namespace XuongMay.ModelViews.AuthModelViews
{
    public class UserLoginModelView
    {
        public required Guid UserId { get; set; }

        public required string ProviderKey { get; set; }

        public required string LoginProvider { get; set; }
    }
}

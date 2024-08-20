namespace XuongMay.ModelViews.AuthModelViews
{
    public class UserTokenModelView
    {
        public required Guid UserId { get; set; }
        public required string Value { get; set; }

        public required string LoginProvider { get; set; }

        public required string Name { get; set; }
    }
}

namespace BlogAppCore.Domain.Entities
{
    public class PostTag
    {
        public PostTag(int postId, int tagId)
        {
            PostId = postId;
            TagId = tagId;
        }

        private PostTag() { }

        public int PostId { get; private set; }

        public Post Post { get; private set; }

        public int TagId { get; private set; }

        public Tag Tag { get; private set; }
    }
}
namespace Sample.CloudDesignPatterns
{
    public class Photo
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public byte[] Thumbnail { get; set; }
    }
}
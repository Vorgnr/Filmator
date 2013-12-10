namespace Filmator.Model.Entities {
    public class MovieStored {
        public int ID { get; set; }
        public bool Seen { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Overview { get; set; }
        public string Homepage { get; set; }

        public bool Adult { get; set; }
    }
}

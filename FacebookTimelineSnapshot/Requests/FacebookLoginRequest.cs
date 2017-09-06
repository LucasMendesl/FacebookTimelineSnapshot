using FacebookTimelineSnapshot.Attributes;

namespace FacebookTimelineSnapshot.Request
{
    public class FacebookLogin
    {
        [FacebookRequestProperty("email")]
        public string Email { get; set; }

        [FacebookRequestProperty("pass")]
        public string Password { get; set; }

        [FacebookRequestProperty("lsd")]
        public string Lsd { get; set; }

        [FacebookRequestProperty("timezone")]
        public string Timezone { get; set; }

        [FacebookRequestProperty("lgndim")]
        public string Lgndim { get; set; }

        [FacebookRequestProperty("lgnrnd")]
        public string Lgnrnd { get; set; }

        [FacebookRequestProperty("lgnjs")]
        public string Lgnjs { get; set; }

        [FacebookRequestProperty("ab_test_data")]
        public string TestData { get; set; }

        [FacebookRequestProperty("locale")]
        public string Locale { get; set; }

        [FacebookRequestProperty("login_source")]
        public string LoginSource { get; set; }

        [FacebookRequestProperty("prefill_contact_point")]
        public string PrefillContactPoint { get; set; }

        [FacebookRequestProperty("prefill_source")]
        public string PrefillSource { get; set; }

        [FacebookRequestProperty("prefill_type")]
        public string PrefillType { get; set; }

        [FacebookRequestProperty("skstamp")]
        public string Skstamp { get; set; }
    }
}



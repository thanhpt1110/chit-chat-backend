namespace ChitChat.Domain.Common
{
    public class LocalizedText
    {
        public string ResourceKey { get; init; } = default!;

        private readonly Dictionary<string, string> Texts = new();

        public static LocalizedText New(string resourceKey) => new LocalizedText() { ResourceKey = resourceKey }
                                .AddEnglishText(resourceKey)
                                .AddDefaultText(resourceKey);

        private LocalizedText AddEnglishText(string text)
        {
            Texts["en-US"] = text;
            return this;
        }

        public LocalizedText AddDefaultText(string text)
        {
            Texts[LocalizationSettings.DefaultCulture] = text;

            return this;
        }

        public string Value
        {
            get
            {
                var culture = Thread.CurrentThread.CurrentCulture;
                return Texts[LocalizationSettings.DefaultCulture];
            }
        }

        public string Format(params object[] args)
        {
            if (!args.Any()) return Value;

            if (string.IsNullOrWhiteSpace(Value)) return Value;

            return string.Format(Value, args);
        }

        public override string ToString() => Value;
    }
}

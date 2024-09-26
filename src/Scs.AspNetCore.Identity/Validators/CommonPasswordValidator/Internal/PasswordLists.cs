using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal
{
    internal class PasswordLists
    {
        private const string Prefix = nameof(CommonPasswordValidator) + ".PasswordLists.";

        private readonly int _requiredLength;
        private readonly ILogger<PasswordLists> _logger;

        public PasswordLists(IOptions<IdentityOptions> options, ILogger<PasswordLists> logger)
        {
            _requiredLength = options.Value.Password.RequiredLength;
            _logger = logger;
            Top100Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("10_million_password_list_top_100.txt"));
            Top500Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("10_million_password_list_top_500.txt"));
            Top1000Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("10_million_password_list_top_1000.txt"));
            Top10000Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("10_million_password_list_top_10000.txt"));
            Top100000Passwords = new Lazy<HashSet<string>>(() => LoadPasswordList("10_million_password_list_top_100000.txt"));
        }

        public Lazy<HashSet<string>> Top100Passwords { get; }
        public Lazy<HashSet<string>> Top500Passwords { get; }
        public Lazy<HashSet<string>> Top1000Passwords { get; }
        public Lazy<HashSet<string>> Top10000Passwords { get; }
        public Lazy<HashSet<string>> Top100000Passwords { get; }

        private HashSet<string> LoadPasswordList(string listName)
        {
            HashSet<string> hashset;

            using (var stream = ReadResource(typeof(PasswordLists).GetTypeInfo().Assembly, "Validators.CommonPasswordValidator.PasswordLists", listName))
            {
                using var streamReader = new StreamReader(stream);
                hashset = new HashSet<string>(
                    GetLines(streamReader),
                    StringComparer.OrdinalIgnoreCase);
            }

            _logger.LogDebug("Loaded {NumberCommonPasswords} common passwords from resource {ResourceName}", hashset.Count, listName);
            return hashset;
        }

        private IEnumerable<string> GetLines(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line.Length >= _requiredLength)
                {
                    yield return line;
                }
            }
        }

        public static Stream ReadResource(Assembly assembly, string folder, string fileName)
        {
            string resourcePath;

            var assemblyName = assembly.GetName().Name;
            resourcePath = folder is not null ? $"{assemblyName}.{folder}.{fileName}" : $"{assemblyName}.{fileName}";

            return assembly.GetManifestResourceStream(resourcePath);
        }
    }
}
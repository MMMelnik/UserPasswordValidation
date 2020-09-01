using System;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace service
{
    public class CustomUserNameValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        // This method validates users. It allows in two users, test@gmail.com and test@ukr.net with passwords test.
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            if (!IsValidEmail(userName))
            {
                throw new FaultException("Email address is invalid!");
            }

            if (!IsValidPassword(password))
            {
                throw new FaultException("Password is invalid!");
            }

            // Instead of a database call 
            if (!(userName == "test@gmail.com" && password == "test") && !(userName == "test@ukr.net" && password == "test"))
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return HasSpecialCharacters(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static bool IsValidPassword(string password)
        {
            return !HasSpecialCharacters(password);
        }

        private static bool HasSpecialCharacters(string str)
        {
            return Regex.IsMatch(str,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}

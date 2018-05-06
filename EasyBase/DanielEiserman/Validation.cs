using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielEiserman.Validation
{
	public static class Validation
	{
		/// <summary>
		/// Kollar om den inskickade adressen är en giltig e-postadress.
		/// </summary>
		/// <param name="email">E-postadress som ska kontrolleras.</param>
		/// <returns>True om den inskickade adressen är en giltig e-post, annars false.</returns>
		public static bool IsEmail(string email)
		{
			bool containOneAt = false;
			bool atNotFirst = false;
			bool dotNotFirst = false;
			bool dotAfterAt = false;
			bool charBetweenAtAndDot = false;
			bool domainChars = false;
            //bool containsIllegalChars = false;

            // Å, Ä och Ö är felaktiga tecken
		    if (email.ToLower().Contains("å") || email.ToLower().Contains("ä") || email.ToLower().Contains("ö"))
		    {
		        return false;
		    }

			char c = ' ';
			char lastC = ' ';
			int nDomainChars = 0;

			for (int i = 0; i <= email.Length - 1; i++) {
				lastC = c;
				c = email[i];

//                if (!char.IsLetterOrDigit(c) && c != '@' && c != '.') {
//                    containsIllegalChars = true;
//                    break;
//                }

			    if (c == ' ')
			    {
			        return false;
			    }

				if (i == 0) { // Första tecknet får inte vara '@' eller '.'
					if (c == '@') {
						return false;
					}
					else if (c == '.') {
						return false;
					}
					atNotFirst = true;
					dotNotFirst = true;
				}
				else {
					if (c == '@') {
						if (containOneAt) { // Om det finns två '@' så är det ingen giltig email
							return false;
						}
						if (lastC == '.') { // Om tecknet innan är en punkt är det ingen giltig email
							return false;
						}
						containOneAt = true;
					}
					else if (c == '.') {
						if (lastC == '@' || lastC == '.') { // Om tecknet innan är snabela eller punkt är det ingen giltig email
							return false;
						}
						if (containOneAt) {
							dotAfterAt = true;
							charBetweenAtAndDot = true;
						}
						nDomainChars = 0;
					}
					else { // Vanliga tecken
						if (dotAfterAt) {
							nDomainChars++;
						}
					}
				}
			}

			domainChars = nDomainChars >= 2;

            //return containOneAt && atNotFirst && dotNotFirst && dotAfterAt && charBetweenAtAndDot && domainChars && !containsIllegalChars;
            return containOneAt && atNotFirst && dotNotFirst && dotAfterAt && charBetweenAtAndDot && domainChars;
        }
	}
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace EMServices
{
    public class CommonUtility
    {

        #region"CreateSalt                          "

        /// <summary>
        /// The Purpose Of CreateSalt Method was Creating Symmetric Key
        /// Symmetric key encryption uses same key, called secret key, for both encryption and decryption. 
        /// </summary>
        /// <param name="username">
        /// Login username as parameter to the CreateSalt Method.
        /// using this username CreateSalt method generates the SECRET KEY
        /// </param>
        /// <returns>
        /// CreateSalt Method return the generated SECRET KEY
        /// </returns>
        public static string CreateSalt(string username)
        {
            //Checking UserName Length
            if (username.Length > 5)
                //If UserLength was greater than 5 Converting into two substing
                username = username.Substring(0, 5);
            else if (username.Length < 5) // If UserName less than 5 padding the user name 
            //with default value
            {
                int add = 5 - username.Length;
                for (int i = 0; i < add; i++)
                    username = username + i;
            }

            //Create a new instance of UnicodeEncoding to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding uE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] key = uE.GetBytes(username.ToLower());

            //Converting byte Array into String Field.
            return Convert.ToBase64String(key);

        }
        #endregion

        #region"CreatePasswordHash                  "
        /// <summary>
        /// The Purpose Of CreatePasswordHash Method :- 
        /// Creates Hash value based on supplied Password and Salt Value
        /// </summary>
        /// User Password and Symmetric key value are parameters to the CreatePasswordHash method 
        /// <param name="pwd"></param>
        ///  Login Password
        /// <param name="salt">
        /// Symmetric Key value(Secret Key)
        /// </param>
        /// <returns>
        /// CreatePasswordHash method return generated hash value.
        /// </returns>
        public static string CreatePasswordHash(string pwd, string salt)
        {
            return string.Join("", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Concat(pwd, salt))).Select(x => x.ToString("X2"))).ToUpper().Substring(0, 16);
        }
        #endregion

        #region"PhoneNumberLabel                    "
        public static string PhoneNumberLabel(String phno)
        {
            if (!string.IsNullOrEmpty(phno))
            {
                List<char> digis = (from c in phno where char.IsDigit(c) select c).ToList<char>();
                string ClearPhNo = string.Empty;
                foreach (var digi in digis)
                {
                    ClearPhNo += digi;
                }
                if (ClearPhNo.Length == 10)
                {
                    phno = string.Format("{0}-{1}-{2}", ClearPhNo.Substring(0, 3), ClearPhNo.Substring(3, 3), ClearPhNo.Substring(6, 4));

                }
            }
            return phno;
        }
        #endregion

    }

    public class PwdGenerator
    {
        #region"Variables                           "
        // Define default min and max password lengths.
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        // Define supported password characters divided into groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789";
        //private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";
        private static string PASSWORD_CHARS_SPECIAL = "!@#$%^&*()_+";
        #endregion

        #region"Generate                            "
        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random. It will be no shorter than the minimum default and
        /// no longer than maximum default.
        /// </remarks>
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH);
        }
        #endregion

        #region"Generate                            "

        /// <summary>
        /// Generates a random password of the exact length.
        /// </summary>
        /// <param name="length">
        /// Exact password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        public static string Generate(int length)
        {
            return Generate(length, length);
        }
        #endregion

        #region"Generate                            "
        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <param name="minLength">
        /// Minimum password length.
        /// </param>
        /// <param name="maxLength">
        /// Maximum password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random and it will fall with the range determined by the
        /// function parameters.
        /// </remarks>
        public static string Generate(int minLength, int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types.
            char[][] charGroups = new char[][]
            {
                PASSWORD_CHARS_LCASE.ToCharArray(),
                PASSWORD_CHARS_UCASE.ToCharArray(),
                PASSWORD_CHARS_NUMERIC.ToCharArray(),
                PASSWORD_CHARS_SPECIAL.ToCharArray()
            };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                randomBytes[1] << 16 |
                randomBytes[2] << 8 |
                randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group. Initially, we will skip
            // special characters.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                        charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                            charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                            leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }
        #endregion
    }
}
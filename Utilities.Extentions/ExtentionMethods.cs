using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Extentions
{
    public static class ExtentionMethods
    {
         public static bool Exists(this string val, List<string> listToCheck)
        {
            bool exists = false;

            if (!val.IsNullOrWhiteSpace())
            {
                if (listToCheck.Count > 0)
                {
                    val = val.Trim().ToUpper();
                    List<string> listSorted = new List<string>();
                    listToCheck.ForEach((value) => listSorted.Add(value.Trim().ToUpper()));
                    exists = listSorted.Contains(val);
                }
            }

            return exists;
        }

        public static bool IsNullOrWhiteSpace(this string val)
        {
            return String.IsNullOrWhiteSpace(val);
        }


        public static bool IsNotNullOrWhiteSpace(this string val)
        {
            return !val.IsNullOrWhiteSpace();
        }

        public static string IsNullOrWhiteSpaceReplace(this string val, string valToReplace = "")
        {
            string valueToReturn = !val.IsNullOrWhiteSpace() ? val : valToReplace;
            return valueToReturn;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static string ToStringJson(this object val)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(val);
        }

        public static string JSAddToList(this string val, int numToAdd, bool shouldBeUniquie = true, Predicate<List<int>> predicate = null)
        {

            if (predicate == null)
            {
                predicate = (list) => true;
            }


            List<int> ints = val.IsNullOrWhiteSpaceReplace("[]").Deserialize<List<int>>();

            if (shouldBeUniquie)
            {
                if (!ints.Contains(numToAdd) && predicate(ints))
                {
                    ints.Add(numToAdd);
                }
            }
            else
            {
                if (predicate(ints))
                {
                    ints.Add(numToAdd);
                }
            }

            val = ints.ToStringJson();

            return val;
        }


        public static string JSRemoveFromList(this string val, int numToRemove, bool removeAll = false, Predicate<List<int>> predicate = null)
        {

            if (predicate == null)
            {
                predicate = (list) => true;
            }

            List<int> ints = val.IsNullOrWhiteSpaceReplace("[]").Deserialize<List<int>>();

            if (ints.Contains(numToRemove))
            {
                if (removeAll && predicate(ints))
                {
                    while (ints.Contains(numToRemove))
                    {
                        ints.Remove(numToRemove);
                    }
                }
                else
                {
                    if (predicate(ints))
                    {
                        ints.Remove(numToRemove);
                    }
                }
            }

            val = ints.ToStringJson();

            return val;
        }

        public static T Deserialize<T>(this string jsonString, bool defaultNullOrEmptyStringToEmptyArray = false)
        {
            if (defaultNullOrEmptyStringToEmptyArray)
            {
                jsonString = jsonString.IsNullOrWhiteSpaceReplace("[]");
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T ToObject<T>(this string jsonString, bool defaultNullOrEmptyStringToEmptyArray = false)
        {
            return Deserialize<T>(jsonString, defaultNullOrEmptyStringToEmptyArray);
        }
    }
}

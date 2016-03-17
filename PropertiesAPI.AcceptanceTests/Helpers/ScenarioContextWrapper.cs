using TechTalk.SpecFlow;

namespace PropertiesAPI.AcceptanceTests.Helpers
{
    public class ScenarioContextWrapper
    {
        public static void SaveValue<T>(string key, T value)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                ScenarioContext.Current[key] = value;
            }
            else
            {
                ScenarioContext.Current.Add(key, value);
            }
        }

        public static T GetVlaue<T>(string key)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                return ScenarioContext.Current.Get<T>(key);
            }

            return default(T);
        }
    }
}

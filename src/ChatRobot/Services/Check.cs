namespace ChatRobot.Services
{
    public class Check
    {
        public static void IsNotNull(object obj, string name)
        {
            if(obj == null)
                throw new ArgumentNullException(name);
        }
    }
}

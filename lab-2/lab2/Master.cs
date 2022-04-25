using System.Collections.Generic;

namespace Lab2
{
    public class Master
    {
        public string name;
        public string birthday;
        public List<Cat> cats;
        public long id;
        public Master(string newName, string newBirthday)
        {
            name = newName;
            birthday = newBirthday;
        }

        public string Getname()
        {
            return name;
        }

        public void Setname(string newValue)
        {
            name = newValue;
        }

        public string Getbirthday()
        {
            return birthday;
        }

        public void Setbirthday(string newValue)
        {
            birthday = newValue;
        }

        public List<Cat> Getcats()
        {
            return cats;
        }

        public void Setcats(List<Cat> newValue)
        {
            cats = newValue;
        }

        public long Getid()
        {
            return id;
        }

        public void Setid(long newValue)
        {
            id = newValue;
        }
    }
}

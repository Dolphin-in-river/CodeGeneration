using System.Collections.Generic;

namespace Lab2
{
    public class Cat
    {
        public string name;
        public string species;
        public string birthday;
        public string color;
        public long masterId;
        public List<Cat> friends;
        public long id;
        public Cat(string Name, string Species, string Birthday, string Color, long MasterId)
        {
            name = Name;
            species = Species;
            birthday = Birthday;
            color = Color;
            masterId = MasterId;
        }

        public string Getname()
        {
            return name;
        }

        public void Setname(string newValue)
        {
            name = newValue;
        }

        public string Getspecies()
        {
            return species;
        }

        public void Setspecies(string newValue)
        {
            species = newValue;
        }

        public string Getbirthday()
        {
            return birthday;
        }

        public void Setbirthday(string newValue)
        {
            birthday = newValue;
        }

        public string Getcolor()
        {
            return color;
        }

        public void Setcolor(string newValue)
        {
            color = newValue;
        }

        public long GetmasterId()
        {
            return masterId;
        }

        public void SetmasterId(long newValue)
        {
            masterId = newValue;
        }

        public List<Cat> Getfriends()
        {
            return friends;
        }

        public void Setfriends(List<Cat> newValue)
        {
            friends = newValue;
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

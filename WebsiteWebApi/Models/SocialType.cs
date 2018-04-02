using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models
{
    public class SocialType //: Enumeration
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CSS { get; set; }

        public int SocialPortalId { get; set; }
        public IEnumerable<SocialPortal> SocialPortals { get; set; }
        //public static SocialType Facebook = new SocialType(1, "facebook");
        //public static SocialType Twitter = new SocialType(2, "twitter");
        //public static SocialType Github = new SocialType(3, "github");
        //public static SocialType Googleplus = new SocialType(3, "googleplus");
        //public static SocialType Linkedin = new SocialType(3, "linkedin");

        //protected SocialType() { }

        //public SocialType(int id, string name)
        //    : base(id, name)
        //{
        //}

        //public static IEnumerable<SocialType> List()
        //{
        //    return new[] { Facebook, Twitter, Github, Googleplus, Linkedin };
        //}
    }

    //public abstract class Enumeration : IComparable
    //{
    //    public string Name { get; }
    //    public int Id { get; }

    //    protected Enumeration()
    //    {
    //    }

    //    protected Enumeration(int id, string name)
    //    {
    //        Id = id;
    //        Name = name;
    //    }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }

    //    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    //    {
    //        var type = typeof(T);
    //        var fields = type.GetTypeInfo().GetFields(BindingFlags.Public |
    //            BindingFlags.Static |
    //            BindingFlags.DeclaredOnly);
    //        foreach (var info in fields)
    //        {
    //            var instance = new T();
    //            var locatedValue = info.GetValue(instance) as T;
    //            if (locatedValue != null)
    //            {
    //                yield return locatedValue;
    //            }
    //        }
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        var otherValue = obj as Enumeration;
    //        if (otherValue == null)
    //        {
    //            return false;
    //        }
    //        var typeMatches = GetType().Equals(obj.GetType());
    //        var valueMatches = Id.Equals(otherValue.Id);
    //        return typeMatches && valueMatches;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }

    //    public int CompareTo(object other)
    //    {
    //        return Id.CompareTo(((Enumeration)other).Id);
    //    }

    //    // Other utility methods ...
    //}
}

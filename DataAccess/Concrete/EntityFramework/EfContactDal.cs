using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Contact;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfContactDal : EfEntityRepositoryBase<Contact, DataBaseContext>, IContactDal
    {       
        public void Add(Contact contact)
        {
            using (var context = new DataBaseContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }

        public void Update(Contact contact)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Contacts
                              where r.Id == contact.Id
                              select r).FirstOrDefault();

                if (result != null)
                {
                    result.Name = contact.Name;
                    result.Email = contact.Email;
                    result.Subject = contact.Subject;
                    result.Message = contact.Message;
                    result.Status = contact.Status;                    
                    result.UpdateDate = contact.UpdateDate;
                    result.UpdateUserId = contact.UpdateUserId;

                    context.SaveChanges();
                }
            }
        }

        public void UpdateStatus(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Contacts
                              where r.Id == id
                              select r).FirstOrDefault();

                result.Status = false;

                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Contacts
                              where r.Id == id
                              select r).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public List<ContactViewDto> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Contacts
                              join u in context.Users on r.UpdateUserId equals u.Id into users
                              from u in users.DefaultIfEmpty()
                              where r.Deleted == false
                              orderby r.CreateDate descending
                              select new
                              {
                                  r.Id,
                                  r.Name,
                                  r.Email,
                                  r.Subject,
                                  r.Message,
                                  r.Status,                                  
                                  r.CreateDate,
                                  FirstName = u == null ? null : u.FirstName,
                                  LastName = u == null ? null : u.LastName
                              }).ToList();

                List<ContactViewDto> resumeList = new List<ContactViewDto>();

                foreach (var r in result)
                {
                    resumeList.Add(new ContactViewDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Email = r.Email,
                        Subject = r.Subject,
                        Message = r.Message,
                        Status = r.Status,                        
                        CreateDate = r.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                        UpdateUser = r.FirstName + " " + r.LastName
                    });
                }

                return resumeList;
            }
        }

        public ContactViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Contacts
                              join u in context.Users on r.UpdateUserId equals u.Id into users
                              from u in users.DefaultIfEmpty()
                              where r.Id == id
                              orderby r.CreateDate descending
                              select new
                              {
                                  r.Id,
                                  r.Name,
                                  r.Email,
                                  r.Subject,
                                  r.Message,
                                  r.Status,
                                  r.CreateDate,
                                  FirstName = u == null ? null : u.FirstName,
                                  LastName = u == null ? null : u.LastName
                              }).FirstOrDefault();

                ContactViewDto contactList = new ContactViewDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    Subject = result.Subject,
                    Message = result.Message,
                    Status = result.Status,
                    CreateDate = result.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                    UpdateUser = result.FirstName + " " + result.LastName
                };
                
                return contactList;
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exist = context.Contacts.Any(r => r.Id == id);
                return exist;
            }
        }
    }
}

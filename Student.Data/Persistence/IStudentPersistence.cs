using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Data.Persistence
{
    public interface IStudentPersistence
    {
        Guid Add(Models.Student todo);
        void Delete(Guid id);
        List<Models.Student> List();
        bool Update(Models.Student todo);
        Models.Student Get(Guid id);
    }
}

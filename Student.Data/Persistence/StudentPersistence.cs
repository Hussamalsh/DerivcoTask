using System;
using System.Collections.Generic;
using System.Linq;
using Student.Data.Models;

namespace Student.Data.Persistence
{
    public class StudentPersistence : IStudentPersistence
    {
        AbstractCrudDB _protobufDb;

        public StudentPersistence()
        {
            _protobufDb = new ProtobufDB(StudentConstants.DefaultDataPath, "bin");
        }

        public Guid Add(Models.Student student)
        {
            var filename = _protobufDb.Write<Models.Student>(student, student.ID.ToString());
            return (student.ID);
        }

        public void Delete(Guid id)
        {
            _protobufDb.Delete<Models.Student>(id.ToString());
        }

        public Models.Student Get(Guid id)
        {
            return _protobufDb.Read<Models.Student>(id.ToString());
        }

        public List<Models.Student> List()
        {
            return (_protobufDb.Read<Models.Student>().ToList());
        }

        public bool Update(Models.Student student)
        {
            Guid id = Add(student);
            return (true);
        }
    }
}

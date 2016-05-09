﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Models;

namespace TrackingSystem.Services.Contracts
{
    public interface IStudentsService
    {
        /// <summary>
        /// Returns all students that arent currently in any group
        /// </summary>
        /// <returns>ApplicationUser</returns>
        ICollection<ApplicationUser> GetGroupAvaiableStudents();

        /// <summary>
        ///  Adds Student to specific group
        /// </summary>
        /// <param name="teacher"> The teacher to add the student</param>
        /// <param name="student"> The student to be added</param>
        void AddStudentToGroup(Teacher teacher, Student student);

        /// <summary>
        /// Returns the specific student
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <returns></returns>
        Student Get(string id);

        /// <summary>
        /// Returns all students
        /// </summary>
        /// <returns>All students</returns>
        IEnumerable<Student> GetAll();
    }
}
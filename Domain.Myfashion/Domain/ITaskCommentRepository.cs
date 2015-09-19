using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Myfashion.Domain
{
   public interface ITaskCommentRepository
    {
        void addTaskComment(TaskComment taskcomment);
        ArrayList getAllTasksCommentOfUser(Guid TaskId);
    }
}

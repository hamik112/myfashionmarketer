using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Myfashion.Domain;
using NHibernate.Transform;
using System.Collections;
using System.Data;
using NHibernate.Linq;
using Api.Myfashionmarketer.Helper;

namespace Api.Myfashionmarketer.Models
{
    public class FbPageCommentRepository
    {
        /// <addFbPageComment>
        /// Add new FbPageComment
        /// </summary>
        /// <param name="fbmsg">Set Values in a FbPageComment Class Property and Pass the same Object of FbPageComment Class.(Domain.FbPageComment)</param>
        public void addFbPageComment(Domain.Myfashion.Domain.FbPageComment _FbPageComment)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_FbPageComment);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


        public List<Domain.Myfashion.Domain.FbPageComment> GetPostComments(string postid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to, get all wall post of Facebook User.
                        List<Domain.Myfashion.Domain.FbPageComment> alst = session.CreateQuery("from FbPageComment where PostId = :PostId")
                         .SetParameter("PostId", postid)
                         .List<Domain.Myfashion.Domain.FbPageComment>()
                         .ToList<Domain.Myfashion.Domain.FbPageComment>();
                        return alst;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        public bool IsFbPagePostCommentLikerExist(Domain.Myfashion.Domain.FbPagePostCommentLiker _FbPagePostCommentLiker)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    try
                    {
                        //Proceed action to, get all wall post of Facebook User.
                        List<Domain.Myfashion.Domain.FbPagePostCommentLiker> alst = session.CreateQuery("from FbPagePostCommentLiker where UserId = :userid and FromId = :fromid and CommentId = :commentid")
                         .SetParameter("userid", _FbPagePostCommentLiker.UserId)
                         .SetParameter("fromid", _FbPagePostCommentLiker.FromId)
                         .SetParameter("commentid", _FbPagePostCommentLiker.CommentId)
                         .List<Domain.Myfashion.Domain.FbPagePostCommentLiker>()
                         .ToList<Domain.Myfashion.Domain.FbPagePostCommentLiker>();
                        if (alst.Count > 0)
                            return true;
                        else
                            return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End session
        }

        public bool IsPostCommentExist(Domain.Myfashion.Domain.FbPageComment _FbPageComment)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to, get all wall post of Facebook User.
                        List<Domain.Myfashion.Domain.FbPageComment> alst = session.CreateQuery("from FbPageComment where UserId = :userid and PostId = :postid and FromId = :fromid and CommentId = :commentid")
                         .SetParameter("postid", _FbPageComment.PostId)
                         .SetParameter("userid", _FbPageComment.UserId)
                         .SetParameter("fromid", _FbPageComment.FromId)
                         .SetParameter("commentid", _FbPageComment.CommentId)
                         .List<Domain.Myfashion.Domain.FbPageComment>()
                         .ToList<Domain.Myfashion.Domain.FbPageComment>();
                        if (alst.Count > 0)
                            return true;
                        else
                            return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End session
        }

        public int UpdateFbPageCommentStatus(Domain.Myfashion.Domain.FbPageComment _FbPageComment)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update FbPageComment set Likes =:Likes,UserLikes =:UserLikes where PostId =:PostId and CommentId =:CommentId")
                                                 .SetParameter("Likes", _FbPageComment.Likes)
                                                 .SetParameter("UserLikes", _FbPageComment.UserLikes)
                                                 .SetParameter("CommentId", _FbPageComment.CommentId)
                                                 .SetParameter("PostId", _FbPageComment.PostId)
                                                 .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

    }
}
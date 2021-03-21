using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Visitors
{
    public class MemberExpressionVisitor : ExpressionVisitor
    {
        public MemberInfo GetLastVisitedMember(Expression exp)
        {
            Visit(exp);
            return member;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            member = node.Member;
            return base.VisitMember(node);
        }

        private MemberInfo member;
    }
}

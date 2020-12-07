﻿using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Groups.Queries
{
    [SecuredOperation]
    public class GetGroupsQuery : IRequest<IDataResult<IEnumerable<Group>>>
    {

        public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IDataResult<IEnumerable<Group>>>
        {
            private readonly IGroupRepository _groupDal;
            public GetGroupsQueryHandler(IGroupRepository groupDal)
            {
                _groupDal = groupDal;
            }
            public async Task<IDataResult<IEnumerable<Group>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Group>>(await _groupDal.GetListAsync());
            }
        }
    }
}

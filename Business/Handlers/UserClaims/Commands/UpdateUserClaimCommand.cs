﻿using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    [SecuredOperation]
    public class UpdateUserClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] ClaimId { get; set; }

        public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimDal;

            public UpdateUserClaimCommandHandler(IUserClaimRepository userClaimDal)
            {
                _userClaimDal = userClaimDal;
            }

            public async Task<IResult> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
            {

                var userList = request.ClaimId.Select(x => new UserClaim() { ClaimId = x, UserId = request.UserId });

                await _userClaimDal.BulkInsert(request.UserId, userList);
                await _userClaimDal.SaveChangesAsync();

                return new SuccessResult(Messages.UserClaimUpdated);
            }
        }
    }
}

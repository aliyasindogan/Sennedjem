﻿using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Groups.Commands
{
    [SecuredOperation]
    public class DeleteGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupDal;

            public DeleteGroupCommandHandler(IGroupRepository groupDal)
            {
                _groupDal = groupDal;
            }

            public async Task<IResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToDelete = await _groupDal.GetAsync(x => x.Id == request.Id);

                _groupDal.Delete(groupToDelete);
                await _groupDal.SaveChangesAsync();

                return new SuccessResult(Messages.GroupDeleted);
            }
        }
    }
}

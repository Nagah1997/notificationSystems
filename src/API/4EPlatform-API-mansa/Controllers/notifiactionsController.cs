public class TaskResultController : BaseController
{
    private readonly IMediator _mediator;

    public TaskResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves task execution results and attached documents for a given task.
    /// </summary>
    /// <param name="dto">The request containing the task ID and pagination parameters.</param>
    [HttpGet]
    public async Task<string> Get()
    {
        return  "hi";
    }
}
        /// <summary>
        /// Submits an execution result and attachments for an assigned task.
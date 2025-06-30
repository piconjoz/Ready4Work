namespace backend.Components.JobListing.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Components.JobListing.Services.Interfaces;
using System.ComponentModel.DataAnnotations;


[ApiController]
[Route("api/jobListings")]
[Authorize]
public class JobListingController : ControllerBase
{
    private readonly IJobListingService _jobListingService;

    public JobListingController(IJobListingService jobListingService)
    {
        _jobListingService = jobListingService;
    }

    // GET /api/jobListing/details
    // Get all job listing details to for admin
}
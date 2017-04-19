﻿#if false
using System.Net;
using System.Threading.Tasks;
using Sniper.Http;
using Sniper.Request;
using Sniper.Response;

using System.Collections.Generic;
using Sniper.ApiClients;
using Sniper.Repositories;


namespace Sniper
{
    /// <summary>
    /// A client for GitHub's Organization Teams API.   //TODO: Replace with TargetProcess if this is usable
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Organization Teams API documentation</a> for more information.  //TODO: Replace with TargetProcess if this is usable
    /// </remarks>
    public class TeamsClient : ApiClient, ITeamsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs Team API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public TeamsClient(IApiConnection apiConnection) : base(apiConnection) {}

        /// <summary>
        /// Gets a single <see cref="Team"/> by identifier.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#get-team  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <returns>The <see cref="Team"/> with the given identifier.</returns>
        public Task<Team> Get(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Get<Team>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list teams of.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAll(string org)
        {
            return GetAll(org, ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list teams of.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(org), org);
            Ensure.ArgumentNotNull(ApiClientKeys.Options, options);

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.GetAll<Team>(endpoint, options);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(ApiClientKeys.Options, options);

            var endpoint = ApiUrls.UserTeams();

            return ApiConnection.GetAll<Team>(endpoint, options);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        public Task<IReadOnlyList<User>> GetAllMembers(int id)
        {
            return GetAllMembers(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        public Task<IReadOnlyList<User>> GetAllMembers(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(ApiClientKeys.Options, options);

            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint, options);
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns>A <see cref="TeamMembership"/> result indicating the membership status</returns>
        public async Task<TeamMembership> GetMembership(int id, string login)
        {
            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Get<Dictionary<string, string>>(endpoint).ConfigureAwait(false);
            }
            catch (NotFoundException)
            {
                return TeamMembership.NotFound;
            }

            return response["state"] == "active"
                ? TeamMembership.Active
                : TeamMembership.Pending;
        }


        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        public Task<Team> Update(int id, UpdateTeam team)
        {
            Ensure.ArgumentNotNull(nameof(team), team);
            
            var endpoint = ApiUrls.Teams(id);
            return ApiConnection.Patch<Team>(endpoint, team);
        }

        /// <summary>
        /// Delte a team - must have owner permissions to this
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public Task Delete(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-member">API documentation</a> for more information.  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <exception cref="ApiValidationException">Thrown if you attempt to add an organization to a team.</exception>
        /// <returns>A <see cref="TeamMembership"/> result indicating the membership status</returns>
        public async Task<TeamMembership> AddMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(login), login);

            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Put<Dictionary<string, string>>(endpoint, RequestBody.Empty).ConfigureAwait(false);
            }
            catch (NotFoundException)
            {
                return TeamMembership.NotFound;
            }

            if (response == null || !response.ContainsKey("state"))
            {
                return TeamMembership.NotFound;
            }

            return response["state"] == "active"
                ? TeamMembership.Active
                : TeamMembership.Pending;
        }

        /// <summary>
        /// Removes a <see cref="User"/> from a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#remove-team-member">API documentation</a> for more information.  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to remove from the team.</param>
        /// <returns><see langword="true"/> if the user was removed from the team; <see langword="false"/> otherwise.</returns>
        public async Task<bool> RemoveMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(login), login);

            var endpoint = ApiUrls.TeamMember(id, login);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint).ConfigureAwait(false);

                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id)
        {
            return GetAllRepositories(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(ApiClientKeys.Options, options);

            var endpoint = ApiUrls.TeamRepositories(id);

            return ApiConnection.GetAll<Repository>(endpoint, null, AcceptHeaders.OrganizationPermissionsPreview, options);
        }

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> AddRepository(int id, string organization, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(organization), organization);
            Ensure.ArgumentNotNullOrEmptyString(RepositoryKeys.Repository, repository);

            var endpoint = ApiUrls.TeamRepository(id, organization, repository);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put(endpoint).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repository">Name of the repo.</param>
        /// <param name="permission">The permission to grant the team on this repository.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> AddRepository(int id, string organization, string repository, RepositoryPermissionRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(organization), organization);
            Ensure.ArgumentNotNullOrEmptyString(RepositoryKeys.Repository, repository);

            var endpoint = ApiUrls.TeamRepository(id, organization, repository);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put<HttpStatusCode>(endpoint, permission, string.Empty, AcceptHeaders.OrganizationPermissionsPreview).ConfigureAwait(false);
                return httpStatusCode.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a repository from the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> RemoveRepository(int id, string organization, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(organization), organization);
            Ensure.ArgumentNotNullOrEmptyString(RepositoryKeys.Repository, repository);

            var endpoint = ApiUrls.TeamRepository(id, organization, repository);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint).ConfigureAwait(false);

                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets whether or not the given repository is managed by the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <param name="owner">Owner of the org the team is associated with.</param>
        /// <param name="repository">Name of the repo.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-repo">API documentation</a> for more information.  //TODO: Replace with TargetProcess if this is usable
        /// </remarks>
        /// <returns><see langword="true"/> if the repository is managed by the given team; <see langword="false"/> otherwise.</returns>
        public async Task<bool> IsRepositoryManagedByTeam(int id, string owner, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(nameof(owner), owner);
            Ensure.ArgumentNotNullOrEmptyString(RepositoryKeys.Repository, repository);

            var endpoint = ApiUrls.TeamRepository(id, owner, repository);

            try
            {
                var response = await ApiConnection.Connection.GetResponse<string>(endpoint).ConfigureAwait(false);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
#endif
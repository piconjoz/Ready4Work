# Infrastructure Setup Guide

## Containers
- Backend (ASP.NET)
- Frontend (Nginx serving React pages)
- Database (MySQL)
- File server (Node.js)
- Certbot (Use to refresh SSL certificates for HTTPS)

## GitHub Actions (CI/CD)
- GitHub action will execute when something is pushed to `development` branch.
- Build frontend files and push to `frontend/dist`.
- Build backend docker images with Dockerfile in `./backend/` and save to `backend/` at ec2 instance.
- For backend, the Docker container will be restarted to apply the changes made.
    - Backend will be temporarily down.

## Network Segmentation
- `net_backend_nginx`
    - Nginx <--> Backend
- `net_backend_file`
    - Backend <--> File server
- `net_backend_mysql`
    - Backend <--> MySQL

## Starting Docker containers
- Starting containers  
`docker compose -f docker-compose up <service>`

## Files
- Backend docker image is store in `backend/`.
- Frontend build is stored into `frontend/dist`.
- File server configuration file is stored in `upload-server`.
- `.env` stores all credentials and important data that is being used by docker compose.





# Project B - Secure Microservices Demo (Auth + Tasks)

This scaffold implements a minimal two-microservice demo to practise:
- Secure coding (input validation, least privilege)
- SAST integration (SonarCloud example workflow)
- SCA (Dependabot / Snyk integration)
- Secrets management (Azure Key Vault via Terraform placeholder)
- Containerization (Docker + docker-compose)
- Basic CI templates for GitHub Actions

Services:
- auth-service: C# .NET Minimal API (skeleton)
- tasks-service: Python Flask API (simple CRUD, input validation)

Files included:
- docker-compose.yml
- .github/workflows/sast.yml (SonarCloud example)
- .github/dependabot.yml
- terraform/main.tf (placeholder for Key Vault)
- docs/STEP_BY_STEP.md (first steps to follow)

How to start locally (dev):
1. Build images:
   - auth-service: `docker build -t projectb/auth-service ./auth-service`
   - tasks-service: `docker build -t projectb/tasks-service ./tasks-service`
2. Start with docker-compose:
   - `docker-compose up --build`
3. Visit:
   - Auth service: http://localhost:5001/ (configured port)
   - Tasks service: http://localhost:5000/tasks

NOTE: This scaffold is intentionally minimal and focuses on integration points.
Follow docs/STEP_BY_STEP.md to implement security hardening and CI/CD integrations.

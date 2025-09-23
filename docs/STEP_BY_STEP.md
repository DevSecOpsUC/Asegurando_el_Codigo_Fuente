# Paso a paso - Project B (Microservices secure demo)

## 1. Inicializar repositorio
- `git init`
- Crear repo en GitHub y push inicial:
  - `git remote add origin <URL>`
  - `git add . && git commit -m "Initial scaffold" && git push -u origin main`

## 2. Ejecutar localmente con Docker Compose
- `docker-compose up --build`
- Ver:
  - http://localhost:5000/  (tasks)
  - http://localhost:5001/  (auth)

## 3. Validación de entradas (codificación defensiva)
- tasks-service usa Pydantic para validar `title` y `description`.
- Añade validaciones adicionales: sanitizar inputs, límites de longitud, listas blancas de formatos.

## 4. Principio de mínimo privilegio
- No ejecutes contenedores con root.
- Configura roles mínimos en Key Vault / cloud IAM.
- Usa cuentas administradas o identities (MSI) para servicios en la nube.

## 5. Integrar SAST (SonarCloud)
- Crea cuenta en SonarCloud y genera token.
- Configura `SONAR_TOKEN` en GitHub Secrets.
- Ajusta `.github/workflows/sast.yml` si usas .NET/Python adicionales.

## 6. Integrar SCA
- Habilita Dependabot (ya incluido) o Snyk.
- Revisa PRs automáticos y aplica parches.

## 7. Gestión de secretos (Azure Key Vault)
- Personaliza `terraform/main.tf` con tenant/subscription ids.
- Crear Key Vault y añadir secretos.
- Modifica servicios para leer secretos desde Key Vault (MSAL / Azure SDK) en lugar de variables en repositorio.

## 8. CI/CD y despliegue seguro
- Añade jobs para build -> SAST -> SCA -> push images -> despliegue.
- Protege ramas y habilita revisión obligatoria en PRs.

## Recursos y enlaces útiles
- SonarCloud docs: https://sonarcloud.io
- Dependabot docs: https://docs.github.com/en/code-security/supply-chain-security/keeping-dependencies-updated-automatically
- Azure Key Vault + Terraform examples: https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault

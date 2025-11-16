# Database SQL Schema

```sql
-- ======================================
-- Tabela: users
-- ======================================
CREATE TABLE users (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL,
    "PasswordHash" TEXT NOT NULL,
    "PasswordSalt" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP NOT NULL,
    "Username" VARCHAR(100) NOT NULL,
    "LastLoginAt" TIMESTAMP NOT NULL,
    "FailedLoginAttempts" INT NOT NULL,
    "LockoutEnd" TIMESTAMP NULL,
    "EmailConfirmed" BOOLEAN NOT NULL
);

-- ======================================
-- Tabela: roles
-- ======================================
CREATE TABLE roles (
    "Id" UUID PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP NOT NULL
);

-- ======================================
-- Tabela: permissions
-- ======================================
CREATE TABLE permissions (
    "Id" UUID PRIMARY KEY,
    "Key" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP NOT NULL
);

-- ======================================
-- Tabela: user_roles
-- ======================================
CREATE TABLE user_roles (
    "Id" UUID PRIMARY KEY,
    "UserId" UUID NOT NULL,
    "RoleId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,

    CONSTRAINT fk_user_roles_user FOREIGN KEY ("UserId")
        REFERENCES users("Id") ON DELETE NO ACTION,

    CONSTRAINT fk_user_roles_role FOREIGN KEY ("RoleId")
        REFERENCES roles("Id") ON DELETE NO ACTION
);

-- ======================================
-- Tabela: role_permissions
-- ======================================
CREATE TABLE role_permissions (
    "Id" UUID PRIMARY KEY,
    "RoleId" UUID NOT NULL,
    "PermissionId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,

    CONSTRAINT fk_role_permissions_role FOREIGN KEY ("RoleId")
        REFERENCES roles("Id") ON DELETE NO ACTION,

    CONSTRAINT fk_role_permissions_permission FOREIGN KEY ("PermissionId")
        REFERENCES permissions("Id") ON DELETE NO ACTION
);

-- ======================================
-- Tabela: refresh_tokens
-- ======================================
CREATE TABLE refresh_tokens (
    "Id" UUID PRIMARY KEY,
    "UserId" UUID NOT NULL,
    "Token" TEXT NOT NULL,
    "Expires" TIMESTAMP NOT NULL,
    "Created" TIMESTAMP NOT NULL,
    "CreatedByIp" VARCHAR(50) NOT NULL,
    "Revoked" TIMESTAMP NULL,
    "RevokedByIp" VARCHAR(50),
    "ReplacedByToken" TEXT,

    CONSTRAINT fk_refresh_tokens_user FOREIGN KEY ("UserId")
        REFERENCES users("Id") ON DELETE CASCADE
);

-- ======================================
-- Tabela: black_listed_token
-- ======================================
CREATE TABLE black_listed_token (
    "Id" UUID PRIMARY KEY,
    "Token" TEXT NOT NULL,
    "RevokedAt" TIMESTAMP NOT NULL,
    "UserId" UUID NOT NULL,

    CONSTRAINT fk_blacklisted_user FOREIGN KEY ("UserId")
        REFERENCES users("Id") ON DELETE CASCADE
);
```

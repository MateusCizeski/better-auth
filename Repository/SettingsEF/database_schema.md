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
# JWT & Identity Core Concepts Deep Dive

## 🔍 Claims

**Definition**:  
A claim is a statement about an entity (typically a user) represented as a key-value pair. Claims provide metadata and authorization context.
**Key Types**:

| Type | Description | Example |
|------|-------------|---------|
| **Registered Claims** | Predefined in JWT spec | `sub`, `exp`, `iss` |
| **Custom Claims** | Application-specific | `department`, `clearance_level` |
| **Identity Claims** | From ASP.NET Identity | `email`, `roles` |
**Code Example**:

```csharp
new Claim(JwtRegisteredClaimNames.Email, "user@domain.com");
new Claim("custom:view_reports", "true");
```

---

## 📜 JwtRegisteredClaimNames

**Purpose**: Standardized claim names from [RFC 7519](https://tools.ietf.org/html/rfc7519). Ensures interoperability between systems.
**Common Members**:

```csharp
JwtRegisteredClaimNames.Sub    // Subject (user ID)
JwtRegisteredClaimNames.Jti    // Unique token identifier
JwtRegisteredClaimNames.Exp    // Expiration timestamp
JwtRegisteredClaimNames.Iss    // Issuer
JwtRegisteredClaimNames.Aud    // Audience
```

**Why Use Them**:

- Avoid naming conflicts
- Built-in validation support
- Standard across JWT implementations

---

## 🔑 SymmetricSecurityKey

**Definition**:  
A cryptographic key where **the same secret** is used for both signing and validation. Suitable for single-service architectures.
**Characteristics**:

- Created from a secret string (minimum 256 bits)
- Used with HMAC algorithms (e.g., HS256)
- **Security Critical**: Compromise allows token forgery
**Implementation**:

```csharp
var key = new SymmetricSecurityKey(
   Encoding.UTF8.GetBytes(config["Jwt:Secret"])
);
```

**Best Practices**:

- Store secret in secure vault (not in code)
- Rotate keys periodically
- Use environment variables in production

---

## 🖋️ SigningCredentials

**Role**: Combines security key and algorithm to sign tokens. Acts as the "notary seal" for JWTs.
**Components**:

```csharp
new SigningCredentials(
   securityKey,                // SymmetricSecurityKey
   SecurityAlgorithms.HmacSha256 // Algorithm
)
```

**Common Algorithms**:

| Algorithm | Type | Use Case |
|-----------|------|----------|
| HS256 | Symmetric | Single service |
| RS256 | Asymmetric | Distributed systems |
| ES512 | Elliptic Curve | High-security apps |

---

## 📜 JwtSecurityToken

**Structure**: Container for token components before serialization.
**Construction**:

```csharp
var token = new JwtSecurityToken(
   issuer: "your-api.com",
   audience: "mobile-app",
   claims: claims,
   expires: DateTime.UtcNow.AddHours(1),
   signingCredentials: creds
);
```

**Key Parts**:

1. **Header**: Algorithm + token type (`{"alg": "HS256", "typ": "JWT"}`)
2. **Payload**: Claims + metadata
3. **Signature**: HMAC hash of header+payload

---

## 🖨️ JwtSecurityTokenHandler

**Responsibilities**:

- Serialize tokens to strings
- Validate incoming tokens
- Read token contents
**Key Methods**:

```csharp
// Write token to string
var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
// Validate and read token
var principal = handler.ValidateToken(
   tokenString,
   validationParameters,
   out _
);
```

---

## 🔄 Token Creation Workflow

1. **Collect Claims**  
  User identity + roles + custom data
2. **Create Security Key**  
  Convert secret string to cryptographic key
3. **Generate Credentials**  
  Pair key with signing algorithm
4. **Build Token**  
  Assemble header + payload with claims
5. **Serialize**  
  Convert to compact string format

---

## 🛡️ Security Best Practices

1. **Key Management**  

- Use Azure Key Vault/AWS Secrets Manager
- Never commit secrets to source control

  ```bash
  # Development secret storage
  dotnet user-secrets init
  dotnet user-secrets set "Jwt:Secret" "your-secure-key"
  ```

2. **Token Validation**  

  ```csharp
  options.TokenValidationParameters = new TokenValidationParameters {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ClockSkew = TimeSpan.Zero // Strict expiration
  };
  ```

3. **Transport Security**  

- Always use HTTPS
- Set `Secure` flag on cookies
- Use HSTS headers

---

## 📄 Example Token

```json
// Header
{
 "alg": "HS256",
 "typ": "JWT"
}
// Payload
{
 "sub": "5b8e6f9e-...",
 "name": "John Doe",
 "roles": ["Admin", "User"],
 "exp": 1735689600,
 "iss": "api.example.com",
 "aud": "mobile-app"
}
// Signature
HMACSHA256(
 base64UrlEncode(header) + "." +
 base64UrlEncode(payload),
 secretKey
)

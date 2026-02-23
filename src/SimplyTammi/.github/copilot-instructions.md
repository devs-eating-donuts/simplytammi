Absolutely, Tommy — here is a **complete, production‑ready `copilot-instructions.md` file** tailored specifically for:

- **.NET 10**
- **C#**
- **Blazor Web App (Server)**
- **Tailwind CSS 4.x**
- **Clean architecture**
- **High test coverage**
- **Copilot‑driven code generation**

This file is structured, opinionated, and designed to guide Copilot into producing consistent, maintainable, testable code across your entire application.

You can drop this directly into your repo.

---

# **`copilot-instructions.md`**

# **GitHub Copilot Instructions for .NET 10 Blazor Web App (Server) with Tailwind CSS 4.x**

## **Purpose**
These instructions guide GitHub Copilot to generate high‑quality, maintainable, secure, and well‑tested code for a **.NET 10 C# Blazor Web App (Server)** using **Tailwind CSS 4.x** for styling. Copilot should follow these rules for all generated components, services, pages, models, tests, and documentation.

---

# **1. General Coding Standards**

## **1.1 Language & Framework**
- Use **C# 13** and **.NET 10** features.
- Use **Blazor Web App (Server)** with interactive components.
- Prefer **async/await** for all I/O operations.
- Use **nullable reference types** everywhere.

## **1.2 Code Style**
- Follow **Microsoft .NET coding conventions**.
- Use **PascalCase** for public members and **camelCase** for private fields.
- Keep methods small, focused, and readable.
- Avoid unnecessary abstractions unless they improve clarity or testability.

## **1.3 Documentation**
- Every class, interface, public method, and public property must include **XML documentation comments**.
- Add inline comments for complex logic.
- Provide examples when generating reusable components or services.

---

# **2. Architecture & Project Structure**

## **2.1 Solution Layout**
Copilot should generate code using this structure:

```
/src
  /Server        <-- Blazor Server host, endpoints, DI, EF Core
  /Client        <-- Razor components, pages, UI logic
  /Shared        <-- DTOs, models, validation
  /Infrastructure <-- Data access, repositories, external services
/tests
  /UnitTests
  /ComponentTests
  /IntegrationTests
```

## **2.2 Clean Architecture Principles**
- UI contains minimal logic.
- Business logic lives in **services**.
- Data access lives in **repositories** or EF Core DbContext.
- Shared models are **POCOs** with validation attributes.
- Use **interfaces** for all services and repositories.

## **2.3 Dependency Injection**
- Use **constructor injection** exclusively.
- Never use service locators or static access patterns.

---

# **3. Blazor Web App (Server) Best Practices**

## **3.1 Components**
- Use **partial classes** (`.razor.cs`) for component logic.
- Keep `.razor` files focused on markup and Tailwind classes.
- Use **EventCallback** instead of Action delegates.
- Avoid unnecessary re-renders; override `ShouldRender()` when appropriate.

## **3.2 State Management**
- Use DI services for shared state.
- Avoid static state.
- Use cascading parameters sparingly.

## **3.3 Navigation & Routing**
- Use `NavigationManager` for navigation.
- Use route parameters with validation.

---

# **4. Tailwind CSS 4.x Styling Rules**

## **4.1 Tailwind Usage**
- Use **Tailwind utility classes** for all styling.
- Avoid writing custom CSS unless absolutely necessary.
- Never use inline `style=""` attributes.
- Use responsive classes (`sm:`, `md:`, `lg:`) for layout.

## **4.2 Component Styling Conventions**
- Keep Tailwind classes readable by grouping logically:
  - Layout → Spacing → Typography → Colors → Effects → Responsive
- Example:

```razor
<div class="flex flex-col gap-4 p-6 bg-white rounded-xl shadow-md">
```

## **4.3 Tailwind File Structure**
Copilot should assume:

```
wwwroot/css/tailwind.css   <-- Tailwind entry
wwwroot/css/app.css        <-- Generated output
```

## **4.4 Icons**
- Use **Web Awesome** or **Font Awesome** when icons are needed.
- Icons must be referenced via `<i class="fa-solid fa-user"></i>` or equivalent.

---

# **5. Data Access & APIs**

## **5.1 EF Core**
- Use **EF Core 10**.
- Use migrations for schema changes.
- Use async database operations.
- Use `DbContext` with DI and pooling.

## **5.2 Validation**
- Use data annotations for simple validation.
- Use FluentValidation for complex rules.

---

# **6. Security Requirements**

## **6.1 Authentication & Authorization**
- Use ASP.NET Core Identity or cookie-based auth.
- Protect sensitive endpoints with `[Authorize]`.

## **6.2 Secure Coding**
- Never log sensitive data.
- Validate all user input.
- Use HTTPS-only assumptions.

---

# **7. Testing Requirements**

## **7.1 Unit Tests**
Copilot must generate unit tests for:
- Services
- Repositories
- Utility classes
- Business logic
- API endpoints
- Component logic (via bUnit)

## **7.2 Testing Frameworks**
- Use **xUnit** for unit tests.
- Use **bUnit** for Blazor component tests.
- Use **Moq** or **NSubstitute** for mocking.
- Use **FluentAssertions** for assertions.

## **7.3 Coverage**
- All generated code must include corresponding tests.
- Aim for **90%+ code coverage**.
- Tests must be deterministic, isolated, and readable.

---

# **8. Performance & Reliability**

## **8.1 Performance**
- Avoid unnecessary re-renders.
- Use `IAsyncEnumerable<T>` for streaming data.
- Use caching where appropriate.

## **8.2 Error Handling**
- Use global exception handling middleware.
- Return structured error responses.
- Avoid swallowing exceptions.

---

# **9. Documentation & Comments**

## **9.1 README Generation**
Copilot should generate:
- Setup instructions
- Architecture overview
- Build/test commands
- Deployment instructions

## **9.2 Code Comments**
- Explain non-obvious logic.
- Document assumptions and edge cases.

---

# **10. Git & CI/CD**

## **10.1 Git Practices**
- Use meaningful commit messages.
- Follow conventional commits when possible.

## **10.2 CI/CD**
When asked, Copilot should generate GitHub Actions workflows for:
- Build
- Test
- Lint
- Publish artifacts

---

# **11. Additional Requirements**

## **11.1 Maintainability**
- Prefer small, focused classes.
- Avoid long methods.
- Use interfaces for abstractions.

## **11.2 Extensibility**
- Follow SOLID principles.
- Use clean interfaces and DI.

## **11.3 Prompt Responsiveness**
Copilot should:
- Ask clarifying questions when requirements are ambiguous.
- Suggest improvements when user instructions are incomplete.
- Follow this instruction file unless explicitly told otherwise.

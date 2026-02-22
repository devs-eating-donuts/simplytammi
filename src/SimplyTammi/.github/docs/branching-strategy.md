# Branching Strategy

## Overview
This repository uses a **Git Flow** branching strategy with automated branch protection.

## Branch Structure

```
main (production)
  ↑
  └── develop (integration)
        ↑
        └── feature/* (development)
```

## Rules

### 1. **main** branch
- **Production-ready code only**
- Can only receive PRs from `develop`
- Requires pull request approval
- No direct commits allowed

### 2. **develop** branch
- **Integration branch for features**
- Can only receive PRs from `feature/*` branches
- Requires pull request approval
- No direct commits allowed

### 3. **feature/** branches
- **Individual feature development**
- Created from `develop`
- Merged back to `develop` via PR
- Can be deleted after merge

## Workflow

### Creating a New Feature

1. **Create feature branch from develop:**
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes and commit:**
   ```bash
   git add .
   git commit -m "feat: your feature description"
   ```

3. **Push feature branch:**
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Create PR to develop:**
   - Go to GitHub
   - Create Pull Request
   - **Base:** `develop`
   - **Compare:** `feature/your-feature-name`
   - Request review and merge

5. **Delete feature branch after merge:**
   ```bash
   git checkout develop
   git pull origin develop
   git branch -d feature/your-feature-name
   ```

### Releasing to Production

1. **Create PR from develop to main:**
   - Go to GitHub
   - Create Pull Request
   - **Base:** `main`
   - **Compare:** `develop`
   - Request review and merge

2. **Tag the release (optional but recommended):**
   ```bash
   git checkout main
   git pull origin main
   git tag -a v1.0.0 -m "Release version 1.0.0"
   git push origin v1.0.0
   ```

## Automated Protection

The repository includes a GitHub Actions workflow that automatically:

- ✅ Validates PRs follow the correct branching rules
- ❌ Blocks PRs that violate branch rules
- 💬 Comments on PRs explaining any violations
- ✅ Confirms valid PRs with a success message

## Invalid PR Examples

❌ **These will be blocked:**
- PR from `feature/xyz` → `main` (must go through `develop`)
- PR from `hotfix/bug` → `develop` (must be `feature/*`)
- PR from `main` → `develop` (wrong direction)

✅ **These are allowed:**
- PR from `feature/new-ui` → `develop`
- PR from `feature/api-update` → `develop`
- PR from `develop` → `main`

## Emergency Hotfixes (Optional)

If you need to create hotfix branches that go directly to `main`:

1. Update `.github/workflows/branch-protection.yml`
2. Add `hotfix/*` to the allowed branches for `main`

## Questions?

Contact the repository maintainer or check the workflow file:
`.github/workflows/branch-protection.yml`

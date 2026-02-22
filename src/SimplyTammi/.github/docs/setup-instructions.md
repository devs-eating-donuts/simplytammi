# GitHub Branch Protection Setup Guide

## What Was Created

✅ **GitHub Actions Workflow:** `.github/workflows/branch-protection.yml`
   - Automatically validates all pull requests
   - Blocks invalid PRs
   - Adds helpful comments

✅ **Documentation:** `.github/docs/branching-strategy.md`
   - Complete branching workflow guide
   - Examples and commands

## What You Need to Do on GitHub

### Step 1: Push These Files to GitHub

```bash
git add .github/
git commit -m "feat: add branch protection workflow and documentation"
git push origin feature/initialsetup
```

### Step 2: Create PR to Develop

1. Go to https://github.com/Tommy-TheDev/simplytammi
2. Click **"Pull requests"** → **"New pull request"**
3. Set **base:** `develop`, **compare:** `feature/initialsetup`
4. Create and merge the PR

### Step 3: Merge to Main

1. Create another PR: **base:** `main`, **compare:** `develop`
2. Merge to get the workflow into main

### Step 4: Enable Branch Protection on GitHub

#### Protect `main` branch:

1. Go to: **Settings** → **Branches** → **Add branch protection rule**
2. Branch name pattern: `main`
3. Enable:
   - ☑️ **Require a pull request before merging**
   - ☑️ **Require approvals** (set to 1 if working solo, or more for teams)
   - ☑️ **Require status checks to pass before merging**
     - Add: `validate-pr` (from the workflow)
   - ☑️ **Do not allow bypassing the above settings**
   - ☑️ **Include administrators** (this prevents YOU from bypassing too)
4. Click **Create**

#### Protect `develop` branch:

1. Add another branch protection rule
2. Branch name pattern: `develop`
3. Enable same settings as above
4. Click **Create**

## Testing the Setup

### Test 1: Valid PR (feature → develop)
```bash
git checkout develop
git checkout -b feature/test-feature
echo "test" > test.txt
git add test.txt
git commit -m "test: validation"
git push origin feature/test-feature
```
Create PR to `develop` → ✅ Should succeed

### Test 2: Invalid PR (feature → main)
Try creating a PR from `feature/test-feature` to `main`
→ ❌ Should fail with a helpful message

### Test 3: Invalid PR (non-feature → develop)
```bash
git checkout -b hotfix/test
git push origin hotfix/test
```
Try creating PR to `develop` → ❌ Should fail

## Result

After setup, your repository will enforce:

✅ **feature/*** → **develop** (allowed)
✅ **develop** → **main** (allowed)
❌ **feature/*** → **main** (blocked)
❌ **anything-else** → **develop** (blocked)
❌ **anything-else** → **main** (blocked)

## Need Help?

Check the workflow logs:
- Go to **Actions** tab in GitHub
- Click on the failed workflow
- View the error messages

## Optional: Slack/Discord Notifications

If you want notifications when PRs are blocked, let me know and I'll add that to the workflow!

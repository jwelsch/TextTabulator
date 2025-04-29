# Publishing Checklist

## 1. Run version scripts locally

Run the `list-version.ps1` script locally to see what version needs to be changed.

```
.\list-versions.ps1
```

Then run the `change-versions.ps1` script to change the versions when the assembilies are built next.

```
.\change-versions.ps1 OldMajor.oldMinor.oldPatch.oldBuild NewMajor.newMinor.newPatch.newBuild
```

Example:

```
.\change-versions.ps1 v1.4.2.0 v1.4.3.0
```

Finally run the `list-version.ps1` script again to make sure the versions were changed as expected.

```
.\list-versions.ps1
```

## 2. Update publish.yml

If a new package is to be published, make sure the publish.yml file in the .github/workflows directory has been updated with the new package.

## 3. Commit the version changes locally

Commit the version changes locally.

```
git commit -a -m "Updated version to vNewMajor.newMinor.newPatch.newBuild"
```

## 4. Push the local commit

Push the local commit to the remote.

```
git push origin main
```

## 5. Tag the repo locally

Tag the local repo with a new tag.

```
git tag vNewMajor.newMinor.newPatch
```

Example:

```
git tag v1.4.3
```

## 6. Push the local tag

Push the local tag to the remote repo.

```
git push origin vNewMajor.newMinor.newPatch
```

Example:

```
git push origin v1.4.3
```

## 7. Check the build

Once the tag is pushed, the Github action [Build](https://github.com/jwelsch/TextTabulator/actions/workflows/build.yml) will be executed automatically. Make sure it completes successfully.

## 8. Create a release

On Github, create a release on this page: https://github.com/jwelsch/TextTabulator/releases

- Select the tag that was just pushed from the "Choose a tag" drop down.
- The title should be the same text as the tag (e.g., `v1.4.3`).
- Then enter a description of the release.
- Make sure "Set as latest release" is checked.
- Finally, click the "Publish release" button.

## 9. Check the publish

Once the release is created, the Github action [Publish](https://github.com/jwelsch/TextTabulator/actions/workflows/publish.yml) will be automatically run. Make sure it completes successfully.

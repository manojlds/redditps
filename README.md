#redditps

Reddit provider for Powershell.

The aim is to create a command line Reddit clien, while also showcasing some of the features of Powershell.

Inspired from [redditfs](https://github.com/ianpreston/redditfs)

##Sample screenshot

![alt tag](https://raw.github.com/manojlds/redditps/master/screenshots/redditps_sample.png)

##Documentation

The provider comes with the `reddit:` drive. You can start using the provider by cd'ing into the drive:

```powershell
cd reddit:
```
Once in the reddit: drive, we can navigate to a subreddit of interest, say, powershell:

```powershell
cd powershell
#cd reddit:\powershell
```
Getting the child items here will list the `Hot` items from the subreddit:

```powershell
ls | select -first 10
```

It is necessary to select a finite range, otherwise it will end up fetching ALL the items from the subreddit.

We can get the `New` items using the `-Type` argument:

```powershell
ls -Type New | select -first 5
```

We can get the content of the items listed using their position in the listing:

```powershell
cat 1
cat 1 -InBrowser
```

Using the `-InBrowser` parameter opens the post in the browser.


##What's in store?

- More details for posts
- Get content of external link in terminal
- User log in
- User actions
- What else you need?
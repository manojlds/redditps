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
Getting the child items here will list the first 15 `Hot` items, by default, from the subreddit:

```powershell
ls
ls -all
ls -all | select -First 10
```

The `-all` parameter can be used to fetch all items in the subreddit. The output can be fed into a `select` to get only top 20, 50 and so on, as needed

We can get the `New` items using the `-Type` argument:

```powershell
ls -type new
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
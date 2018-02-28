// if we are not working with localhost, use a relative root_url <-- consider this production
// otherwise use the IIS Express default <-- consider this development

let url = 'http://localhost:64444/' // IIS Express default

// IE work around
if (!String.prototype.startsWith) {
  String.prototype.startsWith = function(searchString, position) {
     position = position || 0;
    return this.indexOf(searchString, position) === position;
  };
}

if (!window.location.href.startsWith("http://localhost")) {
  // must be a production host, use a relative root url
  url = "/"
}
export const ROOT_URL = url;
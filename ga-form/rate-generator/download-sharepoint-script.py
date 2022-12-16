from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.sharepoint.client_context import ClientContext
from office365.sharepoint.files.file import File

url = ""

username = ""
password = ""

sharepointFilePath = ""
localFile = "./ga-rates.xlsx"

ctxAuth = AuthenticationContext(url)
ctxAuth.acquire_token_for_user(username, password)
ctx = ClientContext(url, ctxAuth)
response = File.open_binary(ctx, sharepointFilePath)
with open(localFile, "wb") as local_file:
    local_file.write(response.content)
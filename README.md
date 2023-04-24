<p><img src="https://raw.githubusercontent.com/evnx32/NontanCLI/main/Image/logo_nontan.png" alt="Logo"></p>

<p align="center"><a href="https://choosealicense.com/licenses/mit/"><img src="https://img.shields.io/bower/l/Mi"
      alt="MIT License" /></a>
  <a href="https://img.shields.io/github/repo-size/evnx32/NontanCLI"><img
      src="https://img.shields.io/github/repo-size/evnx32/NontanCLI" alt="Repo Size" /></a>
  <a href="https://app.fossa.com/projects/git%2Bgithub.com%2Fevnx32%2FNontanCLI?ref=badge_shield"><img
      src="https://app.fossa.com/api/projects/git%2Bgithub.com%2Fevnx32%2FNontanCLI.svg?type=shield"
      alt="FOSSA Status" /></a>
  <a
    href="https://app.codacy.com/gh/evnx32/NontanCLI/dashboard?utm_source=gh&amp;utm_medium=referral&amp;utm_content=&amp;utm_campaign=Badge_grade"><img
      src="https://app.codacy.com/project/badge/Grade/23e02b33cf364d7190678d4958267375" alt="Codacy Badge"></a>
  <a href="https://img.shields.io/github/v/release/evnx32/NontanCLI"><img
      src="https://img.shields.io/github/v/release/evnx32/NontanCLI" alt="Release" /></a>
  <img alt="GitHub all releases" src="https://img.shields.io/github/downloads/evnx32/NontanCLI/total?color=fffffff">
</p>

<h2 id="screenshots">Screenshots</h2>
<figure>

  <img src="https://raw.githubusercontent.com/evnx32/NontanCLI/main/Image/Screenshot_8.png" alt="App Screenshot" />
  <figcaption aria-hidden="true">App Screenshot</figcaption>
</figure>
<h2 id="features">Features</h2>
<ul>
  <li>Streaming Mode</li>
  <li>No Ads (ofc)</li>
  <li>Lightweight</li>
  <li>etc (idk how to explain more)</li>
</ul>

<h2 id="requirements-for-ready-to-use-build">Requirements for Ready-to-use build</h2>
<ul>
<li>.NET Core 7</li>
<li>.NET Runtime Desktop</li>
<li>OS: <strong>Windows 10 1809 Update (build 17763)</strong> or later / <strong>Windows 11 (Any builds)</strong></li>
<li>Internet Access: <strong>Yes</strong></li>
</ul>
<h2 id="building-locally-development">Building Locally/Development</h2>
<p>To use NontanCLI, follow these steps:</p>
<ol>
<li>Install .NET Core 7 on your machine.</li>
<li>Clone the NontanCLI repository from GitHub.</li>
<li>Open the solution in Visual Studio 2022.</li>
<li>Build the solution to generate the NontanCLI executable.</li>
<li>Open a command prompt or terminal window.</li>
<li>Navigate to the directory where the NontanCLI executable is located.</li>
<li>Execute NontanCLI commands as needed.</li>
</ol>



<h2 id="installation">Installation</h2>
<ul>
  <li>Download Latest Version of NontanCLI from Release Page</li>
  <li>Run NontanCLI-setup.exe</li>
  <li>Read some text that containt Licenses</li>
  <li>Click Next > Next > Next ( Windows user will relate )</li>
  <li>Finish</li>
  <li>Type NontanCLI for run the app #</li>
</ul>
<h2 id="installation">Environment Variable Path Issue</h2>
<p>If you can’t run with NontanCLI on Command Prompt, check to
  environment variable.</p>
<ul>
  <li>Open the Windows Start menu and search for "Environment Variables".</li>
  <li>Click on "Edit the system environment variables" to open the System Properties window.</li>
  <li>Click on the "Environment Variables" button at the bottom of the window</li>
  <li>In the "User variables" section, check if there is a variable named "Path". If not, click the "New" button to
    create a new variable and enter "Path" as the name.</li>
  <li>Click the "Edit" button to modify the "Path" variable.</li>
  <li>Click the "New" button and enter the path to the NontanCLI directory, which is typically "C:/NontanCLI". Make sure
    to separate the new path from the existing paths with a semicolon (;).</li>
  <li>Click "OK" to close all the windows.
  </li>
</ul>
<p>Now the NontanCLI directory should be added to your system's PATH environment variable. You should be able to run
  NontanCLI from the command line without any issues.
</p>
<h2 id="usage">Usage</h2>
<p>Help Menu</p>
<pre><code class="lang-bash"><span class="hljs-attribute">NontanCLI -h</span>
</code></pre>
<p>Search With Query</p>
<pre><code class="lang-bash">NontanCLI <span class="hljs-_">-s</span> {Query}
</code></pre>
<p>Watch using Episode ID</p>
<pre><code class="lang-bash"><span class="hljs-attribute">NontanCLI</span> -w {<span class="hljs-attribute">Episode</span> ID}
</code></pre>
<p>Trending Discovery</p>
<pre><code class="lang-bash"><span class="hljs-attribute">NontanCLI -t</span>
</code></pre>
<p>Popular Discovery</p>
<pre><code class="lang-bash"><span class="hljs-attribute">NontanCLI -p</span>
</code></pre>
<p>Check Version</p>
<pre><code class="lang-bash"><span class="hljs-attribute">NontanCLI -v</span>
</code></pre>

<h3 id="other-third-party-repositories-and-libraries-used-in-this-project">Other Third-party repositories and libraries used in this project</h3>
<ul>
<li><a href="https://github.com/chaycee/M3U8Proxy"><strong>M3U8Proxy</strong></a> by chaycee</li>
<li><a href="https://github.com/twitchax/aspnetcore.proxy"><strong>AspNetCore.Proxy</strong></a> by twitchax</li>
<li><a href="https://www.newtonsoft.com/json"><strong>Newtonsoft.Json</strong></a> by Newtonsoft</li>
<li><a href="https://github.com/protobuf-net/protobuf-net"><strong>Protobuf-net</strong></a> by protobuf-net</li>
<li><a href="https://restsharp.dev/"><strong>RestSharp</strong></a> by restsharp</li>
<li><a href="https://www.newtonsoft.com/json"><strong>Spectre.Console</strong></a> by spectreconsole</li>
<li><a href="https://stackexchange.github.io/StackExchange.Redis/"><strong>StackExchange.Redis</strong></a> by StackExchange</li>
</ul>



<h2 id="license">License</h2>
<pre><code>MIT License

Copyright (c) 2023 evnxwashere

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the &quot;Software&quot;), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED &quot;AS IS&quot;, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.</code></pre>
<p><a href="https://app.fossa.com/projects/git%2Bgithub.com%2Fevnx32%2FNontanCLI?ref=badge_large"><img
      src="https://app.fossa.com/api/projects/git%2Bgithub.com%2Fevnx32%2FNontanCLI.svg?type=large"
      alt="FOSSA Status" /></a></p>

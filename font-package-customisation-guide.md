customise Readme.MD

customise namespace and sass.namespace in package.json
customise creator, summary, description in package.json

replace all the 'linear-icons' strings in all files with 'your-package-name'
replace all the variable prefixes 'licons-' with your 'custom-variable-prefix-'; mind the last char (dash) 

rename sass/etc/linear-icons.scss to {package-name}.scss

remove Linearicons fons from resources/fonts
copy fonts to resources/fonts
update readme.md in resources/fonts
adjust file ignore line in gitignore, so proper font files are ignored
customise font paths in sass/etc/_path.scss

customise css class prefix in $font-css-prefix in sass/etc/_variables.scss
customise font family in $font-family in sass/etc/_variables.scss

provide vars with font codes in sass/etc/_variables.scss

provide classes in sass/etc/_icons.scss
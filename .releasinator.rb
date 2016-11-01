configatron.product_name = "PayPal .NET SDK"

# List of items to confirm from the person releasing.  Required, but empty list is ok.
configatron.prerelease_checklist_items = [
]

# The directory where all distributed docs are.  Default is '.'
# configatron.base_docs_dir = '.'

def build_method
  if File.exist?(File.expand_path("..", Dir.pwd)+"/DotNet-SDK-Tools")
    CommandProcessor.command("rm -rf "+File.expand_path("..", Dir.pwd)+"/DotNet-SDK-Tools", live_output=true)
  end 
  CommandProcessor.command("git clone git@github.paypal.com:jziaja/DotNet-SDK-Tools.git "+ File.expand_path("..", Dir.pwd)+"/DotNet-SDK-Tools", live_output=true)
  CommandProcessor.command("python -u "+File.expand_path("..", Dir.pwd)+"/DotNet-SDK-Tools/build.py --repo PayPal-NET-SDK --version #{remove_v} --branch master", live_output=true)
end

# The command that builds the sdk.  Required.
configatron.build_method = method(:build_method)

def publish_to_package_manager(version)
  abort("please implement publish_to_package_manager method")
end

# The method that publishes the sdk to the package manager.  Required.
configatron.publish_to_package_manager_method = method(:publish_to_package_manager)


def wait_for_package_manager(version)
end

# The method that waits for the package manager to be done.  Required
configatron.wait_for_package_manager_method = method(:wait_for_package_manager)

# Whether to publish the root repo to GitHub.  Required.
configatron.release_to_github = true

def validate_paths
  @validator.validate_in_path("python")
end

def appveyor_version()
  File.open("appveyor.yml", 'r') do |f|
    f.each_line do |line|
      if line.match (/version: \d+.\d+.\d+.{build}/)
        return line.strip.split(':')[1].strip.split("{")[0]  
      end
    end
  end
end

def remove_v()
  return @current_release.version.split('v')[1]  
end  

def validate_version_match()
  if 'v'+appveyor_version() != @current_release.version+'.'
    Printer.fail("appveyor version #{appveyor_version} does not match changelog version #{@current_release.version}.")
    abort()
  end
    Printer.success("appveyor version #{appveyor_version} matches latest changelog version #{@current_release.version}.")
end

configatron.custom_validation_methods = [
  method(:validate_paths),
  method(:validate_version_match)
]
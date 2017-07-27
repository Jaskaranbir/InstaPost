# -*- mode: ruby -*-
# vi: set ft=ruby :

plugin_dependencies = [
  "vagrant-docker-compose",
  "vagrant-vbguest"
]

needsRestart = false

# Install plugins if required
plugin_dependencies.each do |plugin_name|
  unless Vagrant.has_plugin? plugin_name
    system("vagrant plugin install #{plugin_name}")
    needsRestart = true
    puts "#{plugin_name} installed"
  end
end

# Restart vagrant if new plugins were installed
if needsRestart === true
  exec "vagrant #{ARGV.join(' ')}"
end

Vagrant.configure(2) do |config|
  config.vm.define "instapostvm" do |instapostvm|
    instapostvm.vm.hostname = "instapost"
    instapostvm.vm.box = "bento/ubuntu-16.04"
    # Frontend
    instapostvm.vm.network "forwarded_port", guest: 80, host: 8080, auto_correct: true
    # Backend
    instapostvm.vm.network "forwarded_port", guest: 81, host: 8181, auto_correct: true
    # MSSQLServer
    instapostvm.vm.network "forwarded_port", guest: 82, host: 8282, auto_correct: true

    instapostvm.vm.provider "virtualbox" do |vb|
      vb.name = "instapostvm"
      vb.gui = false
      vb.memory = "4096"
      vb.cpus = 2
    end

    # # Run as non-login shell, sourcing it to /etc/profile instead of /root/.profile
    # # Due to clashing configurations for vagrant and base box.
    # # See: https://github.com/mitchellh/vagrant/issues/1673#issuecomment-28288042
    # instapostvm.ssh.shell = "bash -c 'BASH_ENV=/etc/profile exec bash'"

    instapostvm.vm.provision "shell", inline: "apt-get update"
    instapostvm.vm.provision "docker"
    instapostvm.vm.provision :docker
    instapostvm.vm.provision :docker_compose, yml: ["/vagrant/docker-compose.yml"], rebuild: true, run: "always"
  end
end

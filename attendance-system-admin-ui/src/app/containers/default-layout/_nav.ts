import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Trang chủ',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    },
    attributes: {
      "policyName": "Permissions.Dashboard.View"
    }
  },
  {
    name: 'Danh mục',
    url: '/content',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Lớp học',
        url: '/content/classes',
        attributes: {
          "policyName": "Permissions.Classes.View"
        }
      },
      {
        name: 'Thiết bị',
        url: '/content/devices',
        attributes: {
          "policyName": "Permissions.Devices.View"
        }
      }
    ]
  },
  {
    name: 'Hệ thống',
    url: '/system',
    iconComponent: { name: 'cil-notes' },
    children: [
      {
        name: 'Quyền',
        url: '/system/roles',
        attributes: {
          "policyName": "Permissions.Roles.View"
        }
      },
      {
        name: 'Người dùng',
        url: '/system/users',
        attributes: {
          "policyName": "Permissions.Users.View"
        }
      }
    ]
  },
];
